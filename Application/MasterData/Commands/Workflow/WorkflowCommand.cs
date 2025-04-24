using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NovaExpediente.Application.Common.Interfaces;
//using NovaExpediente.Application.EventBus.Commands.Publish;
using NovaExpediente.Application.MasterData.Commands.UpsertSolicitudes;
using NovaExpediente.Application.Vacaciones.Queries.GetListaVacaciones;
using NovaExpediente.Application.Notifications.Models;
using NovaExpediente.Domain.Entities;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace NovaExpediente.Application.MasterData.Commands.Workflow
{
    public class WorkflowCommand : UpsertSolicitudesCommand
    {
        public string Accion { get; set; }
        public string Comentario { get; set; }

        public class WorkflowCommandHandler : IRequestHandler<WorkflowCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;
            private readonly IFireForgetCommandHandler _fireForgetCommand;
            private readonly ISecurityService _securityService;
            private readonly INotificationService _notificationService;
            private readonly INominaService _nominaService;
            private readonly IMapper _mapper;
            //private readonly IEventBus _eventBus ;


            public WorkflowCommandHandler(INovaExpedienteDbContext context, IFireForgetCommandHandler fireForgetCommand,
                                          ISecurityService securityService, INotificationService notificationService,
                                          INominaService nominaService, IMapper mapper) //, IEventBus eventBus)
            {
                _context = context;
                _fireForgetCommand = fireForgetCommand;
                _securityService = securityService;
                _notificationService = notificationService;
                _nominaService = nominaService;
                _mapper = mapper;
                //_eventBus = eventBus;
            }

            public async Task<int> Handle(WorkflowCommand request, CancellationToken cancellationToken)
            {
                UpsertCategoryCommandHandler guardarSolicitud = new UpsertCategoryCommandHandler(_context);

                SolicitudMasterData solicitud;
                string estadoBitacora = "PE";
                string mensaje = "";
                string response = "";

                // si no se ha creado la solicitud, primero se crea antes de cambiar el estado
                if (!request.IdSolicitud.HasValue || request.IdSolicitud.Value == 0)
                {
                    request.IdSolicitud = await guardarSolicitud.Handle(request, cancellationToken);
                }
                //else
                //{
                //    solicitud = _context.SolicitudMasterData
                //                .Where(p => p.IDSOLICITUDMASTERDATA == request.IdSolicitud)
                //                .First();

                //    request.IdAutor = solicitud.IDUSUARIOCREA;
                //}
                solicitud = _context.SolicitudMasterData
                            .Include(p => p.Colaborador)
                            .Where(p => p.IDSOLICITUDMASTERDATA == request.IdSolicitud)
                            .First();
                request.IdAutor = solicitud.IDUSUARIOCREA;

                // obtener el siguiente estado de la solicitud
                var transicion = _context.TransicionTipoSolicitud
                                    .Include(p => p.TipoSolicitud)
                                    .Include(p => p.ResponsableEspecifico)
                                    .Where(p => p.IDTIPOSOLICITUD == request.IdTipoSolicitud &&
                                                p.ESTADOACTUAL.Equals(request.CodigoEstado) &&
                                                p.ACCION.Equals(request.Accion))
                                    .First();

                // se guarda el comentario en caso de venir en el request
                if (!string.IsNullOrEmpty(request.Comentario))
                    SaveComment(request);

                // obtener el responsable de la solicitud según el siguiente estado
                request.IdResponsable = transicion.IDRESPONSABLEESPECIFICO.HasValue ?
                                                transicion.IDRESPONSABLEESPECIFICO.Value :
                                                await GetResponsible(request.IdResponsable, request.IdAutor, request.IdColaborador, transicion.PERFILRESPONSABLE);

                // se notifica a quien corresponda según el tipo y categoría de la solicitud
                //   if (transicion.ESTADOSIGUIENTE != "AP" && transicion.ESTADOSIGUIENTE != "DE")
                if (transicion.ESTADOSIGUIENTE != "AP")
                {
                    await Notificar(request, transicion, solicitud);
                }

                // se guarda la solicitud con el cambio de estado y el responsable    
                request.CodigoEstado = transicion.ESTADOSIGUIENTE;
                var result = await guardarSolicitud.Handle(request, cancellationToken);

                if (transicion.ESTADOSIGUIENTE.Equals("PE"))
                {
                    // delega la persistencia de la solicitud a otra tarea en el pool de hilos de ejecución
                    _fireForgetCommand.Execute(async db =>
                    {
                        // Ejecuta el SP asignado a la solicitud para almacenar la solicitud en tabla y aplicarla. 
                        await db.PersistirSolicitud(request.IdTipoSolicitud, request.IdSolicitud.Value);

                        if (solicitud.TipoSolicitud.NOMBRE == "Solicitud de vacaciones" && solicitud.Colaborador.IDNOMINA.HasValue)
                        {

                            var vacacion = await db.ObtenerDatosVacaciones(request.IdSolicitud.Value);

                            //Almacenamos la bitacora
                            await db.ActualizaBitacoraVacaciones(true, (int)vacacion["IDNOMINA"], (DateTime)vacacion["FECHAINICIO"],
                                                                          (DateTime)vacacion["FECHAFINAL"], (int)vacacion["CANTIDADDIAS"],
                                                                          (int)request.IdSolicitud, "", estadoBitacora);



                            //envía la peticion al servicio de nominas  
                            try
                            {
                                response = await _nominaService.ActualizaSaldoVacaciones((int)vacacion["IDNOMINA"], (DateTime)vacacion["FECHAINICIO"],
                                                                    (DateTime)vacacion["FECHAFINAL"], (int)vacacion["CANTIDADDIAS"],
                                                                    (long)request.IdSolicitud);
                                mensaje = response;

                            }
                            catch (Exception ex)
                            {
                                mensaje = ex.Message + ex.StackTrace;
                            }


                          
                            if (!string.IsNullOrEmpty(response) && response.Substring(0, 8).IndexOf("OK:") > -1)
                            {
                                estadoBitacora = "AP";
                            }
                            else
                            {
                                estadoBitacora = "RE";
                            }


                            //Actualizamos la bitacora con la respuesta del servicio 
                            await db.ActualizaBitacoraVacaciones(false, (int)vacacion["IDNOMINA"], (DateTime)vacacion["FECHAINICIO"],
                                                                          (DateTime)vacacion["FECHAFINAL"], (int)vacacion["CANTIDADDIAS"],
                                                                          (int)request.IdSolicitud, mensaje, estadoBitacora);

                        }
                    });

                    //PublishEvent @event = new PublishEvent(_context, _eventBus);
                    //@event.Solicitud(solicitud);
                }

                return result;
            }

            private async Task Notificar(WorkflowCommand request, TransicionTipoSolicitud transicion, SolicitudMasterData solicitud)
            {
                switch (transicion.TipoSolicitud.CATEGORIA)
                {
                    // master data
                    case "MD":
                        break;
                    // administrativa
                    case "AD":

                        if (solicitud.TipoSolicitud.NOMBRE != "Registro de vacunación")
                        {
                            await NotificarSolicitudAdministrativa(request, transicion, solicitud);
                        }
                        break;
                    default:
                        break;
                }
            }

            private async Task NotificarSolicitudAdministrativa(WorkflowCommand request, TransicionTipoSolicitud transicion, SolicitudMasterData solicitud)
            {
                MensajeSolicitudAdministrativa mensaje;
                Colaborador nuevoResponsable = GetResponsibleData(transicion, request.IdResponsable);
                Colaborador solicitante = solicitud.Colaborador;
                Colaborador responsableActual = _context.Colaboradores.Find(solicitud.IDUSUARIORESPONSABLE);
                DocumentoSolicitudAdmin doc;

                string nombreNuevoResponsable = $"{nuevoResponsable.NOMBRE.Trim()} {nuevoResponsable.PRIMERAPELLIDO.Trim()} {nuevoResponsable.SEGUNDOAPELLIDO.Trim()}";
                string datosSolicitante = $"{solicitante.NOMBRE.Trim()} {solicitante.PRIMERAPELLIDO.Trim()} {solicitante.SEGUNDOAPELLIDO.Trim()}" +
                                              $" con número de identificación {solicitante.IDENTIFICACION.Trim()}";
                string datosSolicitanteAprobacion = $"{solicitante.NOMBRE.Trim()} {solicitante.PRIMERAPELLIDO.Trim()} {solicitante.SEGUNDOAPELLIDO.Trim()}";
                string nombreResponsableActual = $"{responsableActual.NOMBRE.Trim()} {responsableActual.PRIMERAPELLIDO.Trim()} {responsableActual.SEGUNDOAPELLIDO.Trim()}";

                mensaje = new MensajeSolicitudAdministrativa()
                {
                    CorreoResponsable = nuevoResponsable.CORREO,
                    NombreResponsable = nombreNuevoResponsable,
                    Solicitante = datosSolicitante,
                    SolicitanteAprobacion = datosSolicitanteAprobacion,
                    CorreoSolicitante = solicitante.CORREO,
                    //Observaciones = doc.Descripcion,
                    IdSolicitud = solicitud.IDSOLICITUDMASTERDATA,
                    Estado = transicion.ESTADOSIGUIENTE,
                    DocSolicitud = solicitud.DOCSOLICITUD,
                    NombreResponsableActual = nombreResponsableActual,
                    ObservacionesAprobador = request.Comentario,
                    Accion = transicion.ACCION,
                    CorreoAdicionalNotificar = string.IsNullOrEmpty(transicion.CORREO_CC_NOTIFICAR) ? "" : transicion.CORREO_CC_NOTIFICAR
                };

                switch (solicitud.TipoSolicitud.NOMBRE)
                {
                    case "Constancia de Salario":
                        mensaje.TipoNotificacion = TipoNotificacion.ConstanciaSalario;

                        break;
                    case "Renovación DIMEX":
                        mensaje.TipoNotificacion = TipoNotificacion.RenovacionDIMEX;

                        break;
                    case "Servicios laborales":
                        mensaje.TipoNotificacion = TipoNotificacion.ServiciosLaborales;

                        break;
                    case "Carta de restricción vehicular":
                        mensaje.TipoNotificacion = TipoNotificacion.RestriccionVehicular;

                        break;
                    case "Solicitud de vacaciones":
                        mensaje.TipoNotificacion = TipoNotificacion.Vacaciones;
                        if (mensaje.Estado == "CO")
                        {
                            mensaje.Solicitante = $"{solicitante.NOMBRE.Trim()} {solicitante.PRIMERAPELLIDO.Trim()} {solicitante.SEGUNDOAPELLIDO.Trim()}";
                        }
                        break;
                    case "Registro de incapacidades":
                        mensaje.TipoNotificacion = TipoNotificacion.Incapacidades;
                        if (mensaje.Estado == "CO")
                        {
                            mensaje.Solicitante = $"{solicitante.NOMBRE.Trim()} {solicitante.PRIMERAPELLIDO.Trim()} {solicitante.SEGUNDOAPELLIDO.Trim()}";
                        }
                        break;
                    case "Solicitud de permiso":
                        mensaje.TipoNotificacion = TipoNotificacion.Permisos;
                        if (mensaje.Estado == "CO")
                        {
                            mensaje.Solicitante = $"{solicitante.NOMBRE.Trim()} {solicitante.PRIMERAPELLIDO.Trim()} {solicitante.SEGUNDOAPELLIDO.Trim()}";
                        }
                        break;
                    default:
                        break;
                }

                await _notificationService.SendAsync(mensaje);
            }

            private Colaborador GetResponsibleData(TransicionTipoSolicitud transicion, int idResponsable)
            {
                if (transicion.IDRESPONSABLEESPECIFICO.HasValue)
                {
                    return transicion.ResponsableEspecifico;
                }
                else
                {
                    return _context.Colaboradores.Find(idResponsable);
                }
            }

            /// <summary>
            /// Guarda un comentario asociado a una solicitud
            /// </summary>
            /// <param name="request">Información de la petición recibida</param>
            private void /*async Task*/ SaveComment(WorkflowCommand request) //, CancellationToken cancellationToken)
            {
                SolicitudComentario entity;

                entity = new SolicitudComentario();

                _context.SolicitudComentario.Add(entity);

                entity.IDSOLICITUDMASTERDATA = request.IdSolicitud.Value;
                entity.IDUSUARIOCREA = request.IdResponsable;
                entity.ACCION = request.Accion;
                entity.COMENTARIO = request.Comentario;

                //await _context.SaveChangesAsync(cancellationToken);

            }

            /// <summary>
            /// Obtiene el identificador de la persona responsable que debe atender la solicitud
            /// </summary>
            /// <param name="currentResponsible">Responsable actual</param>
            /// <param name="idAuthor">Identificador del autor de la solicitud</param>
            /// <param name="responsibleRole">Nombre del rol responsable de atender el siguiente estado de una solicitud</param>
            /// <returns></returns>
            private async Task<int> GetResponsible(int currentResponsible, int idAuthor, int? idColaborador, string responsibleRole)
            {
                if (string.IsNullOrEmpty(responsibleRole) || responsibleRole.ToUpper().Equals("SISTEMA"))
                {
                    return currentResponsible;
                }

                if (responsibleRole.ToUpper().Equals("AUTOR"))
                {
                    return idAuthor;
                }

                if (responsibleRole.ToUpper().Equals("EXPEDIENTE.DM"))
                {
                    var dm = _context.Colaboradores
                                     .Where(c => c.IDCOLABORADOR == idColaborador.Value)
                                     .Select(c => c.IDDELIVERYMANAGER)
                                     .First();
                    return dm.Value;
                }
                else
                {


                    //var esNovashore = _context.Colaboradores
                    //                .Where(c => c.IDCOLABORADOR == idColaborador.Value)
                    //                .Select(c => c.ESNOVASHORE)
                    //                .First();

                    //if (esNovashore)
                    //{
                    //    var usuariosEmail = await _securityService.GetUsersWithRole(responsibleRole.ToUpper());
                    //    var colaboradores = _context.Colaboradores
                    //                        .Where(c => usuariosEmail.Contains(c.CORREO) && c.ESNOVASHORE == true)
                    //                        .Select(c => c.IDCOLABORADOR)
                    //                        .ToList();

                    //    Random rnd = new Random();
                    //    int idx = rnd.Next(colaboradores.Count);
                    //    return colaboradores[idx];
                    //}
                    //else
                    //{
                    //    var usuariosEmail = await _securityService.GetUsersWithRole(responsibleRole.ToUpper());
                    //    var colaboradores = _context.Colaboradores
                    //                        .Where(c => usuariosEmail.Contains(c.CORREO))
                    //                        .Select(c => c.IDCOLABORADOR)
                    //                        .ToList();

                    //    Random rnd = new Random();
                    //    int idx = rnd.Next(colaboradores.Count);
                    //    return colaboradores[idx];
                }

                var usuariosEmail = await _securityService.GetUsersWithRole(responsibleRole.ToUpper());
                var colaboradores = _context.Colaboradores
                                    .Where(c => usuariosEmail.Contains(c.CORREO))
                                    .Select(c => c.IDCOLABORADOR)
                                    .ToList();

                Random rnd = new Random();
                int idx = rnd.Next(colaboradores.Count);
                return colaboradores[idx];
            }
        }


        public class DocumentoSolicitudAdmin
        {
            public string NumeroPlaca { get; set; }
            public string DireccionOrigen { get; set; }
            public string DireccionDestino { get; set; }
            public string Descripcion { get; set; }
            public string Estado { get; set; }

            public DateTime FechaInicio { get; set; }
            public DateTime FechaFinal { get; set; }
            public string HoraInicio { get; set; }
            public string HoraFinal { get; set; }
            public int CantDias { get; set; }
            public string NumBoleta { get; set; }
            public string Goce { get; set; }
            public int Tipo { get; set; }
            public string TipoNombre { get; set; }

        }

        public class DocumentoSolicitudIdiomas
        {
            public List<DocumentoSolicitudIdiomasJson> nivelIdiomasList { get; set; }
            public string estado { get; set; }
            public bool edicion { get; set; }


        }
        public class DocumentoSolicitudIdiomasJson
        {
            public int ididioma { get; set; }
            public int idnivelidioma { get; set; }
            public int idcolaborador { get; set; }
            public string estado { get; set; }

        }
    }
}
