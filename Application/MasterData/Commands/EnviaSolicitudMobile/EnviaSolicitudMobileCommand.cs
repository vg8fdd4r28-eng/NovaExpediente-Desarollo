using AutoMapper;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Application.MasterData.Commands.Workflow;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static NovaExpediente.Application.MasterData.Commands.Workflow.WorkflowCommand;
using Newtonsoft.Json;
using NovaExpediente.Application.Colaboradores.Queries.GetColaboradorVacaciones;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace NovaExpediente.Application.MasterData.Commands.EnviaSolicitudMobile
{
    public class EnviaSolicitudMobileCommand : IRequest<int>
    {
        public int idColaborador { get; set; }
        public string tipoSolicitud { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFinal { get; set; }
        public int cantDias { get; set; }

        public string descripcion { get; set; }

        //vacaciones
        public decimal saldoVacaciones { get; set; }
        public decimal nuevoSaldo { get; set; }


        //incapacidades
        public string numboleta { get; set; }
        public string boleta { get; set; }
        public char tipoincapacidad { get; set; }

        //permisos
        public int Tipo { get; set; }
        public string Goce { get; set; }
        public string TipoNombre { get; set; }



        public class UpsertCategoryCommandHandler : IRequestHandler<EnviaSolicitudMobileCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;
            private readonly IFireForgetCommandHandler _fireForgetCommand;
            private readonly ISecurityService _securityService;
            private readonly INotificationService _notificationService;
            private readonly INominaService _nominaService;
            private readonly IMapper _mapper;


            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context, IFireForgetCommandHandler fireForgetCommand,
                                     ISecurityService securityService, INotificationService notificationService,
                                     INominaService nominaService, IMapper mapper) //, IEventBus eventBus)
            {
                _context = context;
                _fireForgetCommand = fireForgetCommand;
                _securityService = securityService;
                _notificationService = notificationService;
                _nominaService = nominaService;
                _mapper = mapper;
            }

            public async Task<int> Handle(EnviaSolicitudMobileCommand request, CancellationToken cancellationToken)
            {
                string tipoSolicitudNombre = "";
                int result = 0;
                DocSolicitud docJson;


                switch (request.tipoSolicitud)
                {
                    case "Vacaciones":
                        tipoSolicitudNombre = "Solicitud de vacaciones";
                        break;

                    case "Permisos":
                        tipoSolicitudNombre = "Solicitud de permiso";
                        break;

                    case "Incapacidades":
                        tipoSolicitudNombre = "Registro de incapacidades";
                        break;

                    case "ConstanciaSalario":
                        tipoSolicitudNombre = "Constancia de Salario";
                        break;

                    default:
                        break;
                }

                var tipoSolicitud = _context.TipoSolicitud
                                     .Where(c => c.NOMBRE == tipoSolicitudNombre)
                                     .FirstOrDefault();


                switch (request.tipoSolicitud)
                {
                    case "Vacaciones":

                        var colaborador = await _context.Colaboradores
              .Where(c => c.IDCOLABORADOR == request.idColaborador)
              .ProjectTo<ColaboradorVacacionesDto>(_mapper.ConfigurationProvider)
              .ToListAsync(cancellationToken);

                        docJson = new DocSolicitud
                        {
                            fechaInicio = request.fechaInicio.ToString("yyyy-MM-dd"),
                            fechaFinal = request.fechaFinal.ToString("yyyy-MM-dd"),
                            idColaborador = request.idColaborador,
                            cantDias = request.cantDias,
                            saldoVacaciones = colaborador[0].SALDOVACACIONESACTUAL,
                            nuevoSaldo = colaborador[0].SALDOVACACIONESACTUAL - request.cantDias,
                            descripcion = request.descripcion

                        };

                        break;

                    case "Permisos":

                        docJson = new DocSolicitud
                        {
                            fechaInicio = request.fechaInicio.ToString("yyyy-MM-dd"),
                            fechaFinal = request.fechaFinal.ToString("yyyy-MM-dd"),
                            idColaborador = request.idColaborador,
                            cantDias = request.cantDias,
                            descripcion = request.descripcion,
                            Tipo = request.Tipo,
                            Goce = request.Goce,
                            TipoNombre = request.TipoNombre,
                        };


                        break;

                    case "Incapacidades":

                        docJson = new DocSolicitud
                        {
                            fechaInicio = request.fechaInicio.ToString("yyyy-MM-dd"),
                            fechaFinal = request.fechaFinal.ToString("yyyy-MM-dd"),
                            idColaborador = request.idColaborador,
                            cantDias = request.cantDias,
                            descripcion = request.descripcion,
                            numboleta = request.numboleta,
                            boleta = request.boleta,
                            tipoincapacidad = request.tipoincapacidad
                        };

                        break;

                    case "ConstanciaSalario":

                        docJson = new DocSolicitud
                        {
                            idColaborador = request.idColaborador,
                            descripcion = request.descripcion,
                            fechaInicio = DateTime.Now.ToString("yyyy-MM-dd"),
                            fechaFinal = DateTime.Now.ToString("yyyy-MM-dd"),
                            cantDias = 0

                        };

                        break;

                    default:
                        docJson = new DocSolicitud();
                        throw new Exception("No existe el tipo de solicitud");
                }





                if (tipoSolicitud != null)
                {




                    WorkflowCommandHandler workflow = new WorkflowCommandHandler(_context, _fireForgetCommand, _securityService, _notificationService, _nominaService, _mapper);




                    WorkflowCommand command = new WorkflowCommand();

                    command.IdSolicitud = null;
                    command.IdTipoSolicitud = tipoSolicitud.IDTIPOSOLICITUD;
                    command.CodigoEstado = "RE";
                    command.IdAutor = request.idColaborador;
                    command.IdResponsable = request.idColaborador;
                    command.IdColaborador = request.idColaborador;
                    command.Accion = "Enviar";
                    command.Comentario = "";
                    command.DocSolicitud = JsonConvert.SerializeObject(docJson, Formatting.Indented);

                    WorkflowCommandValidator workflowValidator = new WorkflowCommandValidator(_context);
                    var resultadoValidacionWorkFlow = workflowValidator.Validate(command);

                    if (!resultadoValidacionWorkFlow.IsValid)
                    {
                        throw new Exception(resultadoValidacionWorkFlow.Errors[0].ToString());
                    }

                    result = await workflow.Handle(command, cancellationToken);


                }
                else
                {
                    throw new Exception("No existe el tipo de solicitud");
                };



                return result;

            }
        }


        private class DocSolicitud
        {
            //generales admin
            public int idColaborador { get; set; }
            public string fechaInicio { get; set; }
            public string fechaFinal { get; set; }
            public int cantDias { get; set; }
            public string descripcion { get; set; }

            //vacaciones
            public decimal saldoVacaciones { get; set; }
            public decimal nuevoSaldo { get; set; }


            //incapacidades
            public string numboleta { get; set; }
            public string boleta { get; set; }
            public char tipoincapacidad { get; set; }

            //permisos
            public int Tipo { get; set; }
            public string Goce { get; set; }
            public string TipoNombre { get; set; }








        }

    }
}
