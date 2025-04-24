using FluentValidation;
using Newtonsoft.Json;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Application.IdiomasColaborador.Queries.GetListaIdiomasColaborador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NovaExpediente.Application.MasterData.Commands.Workflow.WorkflowCommand;

namespace NovaExpediente.Application.MasterData.Commands.Workflow
{
    public class WorkflowCommandValidator : AbstractValidator<WorkflowCommand>
    {
        private readonly INovaExpedienteDbContext _context;
        public WorkflowCommandValidator(INovaExpedienteDbContext context)
        {
            _context = context;
            RuleFor(x => x.IdTipoSolicitud).Must(SolicitudEnTransito).WithMessage("La solicitud no se puede enviar porque el colaborador tiene pendiente de aprobación una solicitud del mismo tipo.");
            RuleFor(x => x.IdTipoSolicitud).Must(SolicitudAdminEnTransito).WithMessage("La solicitud no se puede enviar porque el colaborador tiene pendiente de aprobación una solicitud administrativa del mismo tipo.");
            RuleFor(x => x.IdTipoSolicitud).Must(SolicitudAdminVacacionesExistentes).WithMessage("La solicitud de vacaciones no se puede enviar porque el rango de fechas se traslapa con otras solicitudes previamente creadas.");
            RuleFor(x => x.IdTipoSolicitud).Must(SolicitudAdminIncapacidadesExistentes).WithMessage("La solicitud de incapacidades no se puede enviar porque el número de la boleta ya existe.");
         
            
            
            RuleFor(x => x.IdTipoSolicitud).Must(SolicitudAdminIncapacidadesFechasTraslapadas).WithMessage("La solicitud de incapacidades no se puede enviar porque el rango de fechas se traslapa con otras solicitudes previamente aprobadas.");
            RuleFor(x => x.IdTipoSolicitud).Must(SolicitudAdminPermisosFechasTraslapadas).WithMessage("La solicitud de permisos no se puede enviar porque el rango de fechas se traslapa con otras solicitudes previamente aprobadas.");


            RuleFor(x => x.IdTipoSolicitud).Must(SolicitudAdminVacacionesTraslapeEnPermisos).WithMessage("La solicitud de vacaciones no se puede enviar porque el rango de fechas se traslapa con otras solicitudes de permisos previamente creadas.");
            RuleFor(x => x.IdTipoSolicitud).Must(SolicitudAdminVacacionesTraslapeEnIncapacidades).WithMessage("La solicitud de vacaciones no se puede enviar porque el rango de fechas se traslapa con otras solicitudes de incapacidades previamente creadas.");

            RuleFor(x => x.IdTipoSolicitud).Must(SolicitudAdminPermisosTraslapeEnVacaciones).WithMessage("La solicitud de permisos no se puede enviar porque el rango de fechas se traslapa con otras solicitudes de vacaciones previamente creadas.");
            RuleFor(x => x.IdTipoSolicitud).Must(SolicitudAdminPermisosTraslapeEnIncapacidades).WithMessage("La solicitud de permisos no se puede enviar porque el rango de fechas se traslapa con otras solicitudes de incapacidades previamente creadas.");



            RuleFor(x => x.IdTipoSolicitud).Must(SolicitudIdiomasNivelExistente).WithMessage("La solicitud de idiomas no se puede enviar porque el colaborador ya tiene uno de los idiomas y su nivel. Por favor editar el registro en la opción de incluir.");

        }

        private bool SolicitudEnTransito(WorkflowCommand solicitud, int idTipoSolicitud)
        {
            if (solicitud.CodigoEstado == "RE")
            {

                var TipoSolicitud = _context.TipoSolicitud
                              .Where(s => s.NOMBRE == "Solicitud de vacaciones")
                              .ToList();


                if (idTipoSolicitud == TipoSolicitud[0].IDTIPOSOLICITUD)
                {
                    return true;
                }
                else
                {


                    List<string> estadosEnTransito = new List<string>() { "EV", "EA", "CO" };
                    var solicitudesEnTransito = _context.SolicitudMasterData
                                                .Where(s => s.IDSOLICITUDMASTERDATA != solicitud.IdSolicitud
                                                            && s.IDTIPOSOLICITUD == solicitud.IdTipoSolicitud
                                                            && s.IDCOLABORADOR == solicitud.IdColaborador
                                                            && estadosEnTransito.Contains(s.ESTADOSOLICITUD))
                                                .ToList();

                    if (solicitudesEnTransito.Count() > 0)
                    {
                        return false;
                    }
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private bool SolicitudAdminEnTransito(WorkflowCommand solicitud, int idTipoSolicitud)
        {
            if (solicitud.CodigoEstado == "RE")
            {


                var TipoSolicitud = _context.TipoSolicitud
                              .Where(s => s.NOMBRE == "Solicitud de vacaciones")
                              .ToList();


                if (idTipoSolicitud == TipoSolicitud[0].IDTIPOSOLICITUD)
                {
                    return true;
                }
                else
                {


                    List<string> estadosEnTransito = new List<string>() { "EPV", "EP", "EA", "CO" };
                    var solicitudesEnTransito = _context.SolicitudMasterData
                                                .Where(s => s.IDSOLICITUDMASTERDATA != solicitud.IdSolicitud
                                                            && s.IDTIPOSOLICITUD == solicitud.IdTipoSolicitud
                                                            && s.IDCOLABORADOR == solicitud.IdColaborador
                                                            && estadosEnTransito.Contains(s.ESTADOSOLICITUD))
                                                .ToList();
                    if (solicitudesEnTransito.Count() > 0)
                    {
                        return false;
                    }
                    return true;

                }
            }
            else
            {
                return true;
            }
        }

        private bool SolicitudAdminVacacionesExistentes(WorkflowCommand solicitud, int idTipoSolicitud)
        {
            List<string> estadosEnTransito = new List<string>() { "AP", "EPV", "EP", "EA", "CO" };

            DocumentoSolicitudAdmin doc;

            doc = JsonConvert.DeserializeObject<DocumentoSolicitudAdmin>(solicitud.DocSolicitud);

            if (solicitud.CodigoEstado == "RE")
            {
                var TipoSolicitud = _context.TipoSolicitud
                                         .Where(s => s.NOMBRE == "Solicitud de vacaciones")
                                         .ToList();


                var solicitudesAprobadasDoc = _context.SolicitudMasterData
                                            .Where(s =>
                                                        s.IDTIPOSOLICITUD == TipoSolicitud[0].IDTIPOSOLICITUD
                                                        && s.IDCOLABORADOR == solicitud.IdColaborador
                                                        && estadosEnTransito.Contains(s.ESTADOSOLICITUD)).ToList();

                var resultados = solicitudesAprobadasDoc.Select(s => JsonConvert.DeserializeObject<DocumentoSolicitudAdmin>(s.DOCSOLICITUD)).Where(s => s.FechaInicio <= doc.FechaFinal && s.FechaFinal >= doc.FechaInicio).ToList();


                if (idTipoSolicitud != TipoSolicitud[0].IDTIPOSOLICITUD)
                {
                    return true;
                }

                if (resultados.Count > 0)
                {
                    return false;

                }

                return true;
            }
            else
            {
                return true;
            }



        }


        private bool SolicitudAdminIncapacidadesExistentes(WorkflowCommand solicitud, int idTipoSolicitud)
        {
            List<string> estadosEnTransito = new List<string>() { "AP", "EA" };

            DocumentoSolicitudAdmin doc;

            doc = JsonConvert.DeserializeObject<DocumentoSolicitudAdmin>(solicitud.DocSolicitud);

            if (solicitud.CodigoEstado == "RE")
            {
                var TipoSolicitud = _context.TipoSolicitud
                                         .Where(s => s.NOMBRE == "Registro de incapacidades")
                                         .FirstOrDefault();


                var solicitudesAprobadasDoc = _context.SolicitudMasterData
                                            .Where(s =>
                                                        s.IDTIPOSOLICITUD == TipoSolicitud.IDTIPOSOLICITUD
                                                        // && s.IDCOLABORADOR == solicitud.IdColaborador
                                                        && estadosEnTransito.Contains(s.ESTADOSOLICITUD)).ToList();

                var resultados = solicitudesAprobadasDoc.Select(s => JsonConvert.DeserializeObject<DocumentoSolicitudAdmin>(s.DOCSOLICITUD)).Where(s => s.NumBoleta == doc.NumBoleta).ToList();


                if (idTipoSolicitud != TipoSolicitud.IDTIPOSOLICITUD)
                {
                    return true;
                }

                if (resultados.Count > 0)
                {
                    return false;

                }

                return true;
            }
            else
            {
                return true;
            }



        }

        private bool SolicitudAdminIncapacidadesFechasTraslapadas(WorkflowCommand solicitud, int idTipoSolicitud)
        {
            List<string> estadosEnTransito = new List<string>() { "AP" };

            DocumentoSolicitudAdmin doc;

            doc = JsonConvert.DeserializeObject<DocumentoSolicitudAdmin>(solicitud.DocSolicitud);

            if (solicitud.CodigoEstado == "RE")
            {
                var TipoSolicitud = _context.TipoSolicitud
                                        .Where(s => s.NOMBRE == "Registro de incapacidades")
                                         .FirstOrDefault();


                var solicitudesAprobadasDoc = _context.SolicitudMasterData
                                            .Where(s =>
                                                        s.IDTIPOSOLICITUD == TipoSolicitud.IDTIPOSOLICITUD
                                                        && s.IDCOLABORADOR == solicitud.IdColaborador
                                                        && estadosEnTransito.Contains(s.ESTADOSOLICITUD)).ToList();

                var resultados = solicitudesAprobadasDoc.Select(s => JsonConvert.DeserializeObject<DocumentoSolicitudAdmin>(s.DOCSOLICITUD)).Where(s => s.FechaInicio <= doc.FechaFinal && s.FechaFinal >= doc.FechaInicio).ToList();


                if (idTipoSolicitud != TipoSolicitud.IDTIPOSOLICITUD)
                {
                    return true;
                }

                if (resultados.Count > 0)
                {
                    return false;

                }

                return true;
            }
            else
            {
                return true;
            }



        }

        private bool SolicitudAdminPermisosFechasTraslapadas(WorkflowCommand solicitud, int idTipoSolicitud)
        {
            List<string> estadosEnTransito = new List<string>() { "AP" };

            DocumentoSolicitudAdmin doc;

            doc = JsonConvert.DeserializeObject<DocumentoSolicitudAdmin>(solicitud.DocSolicitud);

            if (solicitud.CodigoEstado == "RE")
            {
                var TipoSolicitud = _context.TipoSolicitud
                                        .Where(s => s.NOMBRE == "Solicitud de permiso")
                                         .FirstOrDefault();


                var solicitudesAprobadasDoc = _context.SolicitudMasterData
                                            .Where(s =>
                                                        s.IDTIPOSOLICITUD == TipoSolicitud.IDTIPOSOLICITUD
                                                        && s.IDCOLABORADOR == solicitud.IdColaborador
                                                        && estadosEnTransito.Contains(s.ESTADOSOLICITUD)).ToList();

                var resultados = solicitudesAprobadasDoc.Select(s => JsonConvert.DeserializeObject<DocumentoSolicitudAdmin>(s.DOCSOLICITUD)).Where(s => s.FechaInicio <= doc.FechaFinal && s.FechaFinal >= doc.FechaInicio).ToList();


                if (idTipoSolicitud != TipoSolicitud.IDTIPOSOLICITUD)
                {
                    return true;
                }

                if (resultados.Count > 0)
                {
                    return false;

                }

                return true;
            }
            else
            {
                return true;
            }



        }


        private bool SolicitudAdminVacacionesTraslapeEnPermisos(WorkflowCommand solicitud, int idTipoSolicitud)
        {
            List<string> estadosEnTransito = new List<string>() { "AP","EPV", "EP", "EA", "CO" };

            DocumentoSolicitudAdmin doc;

            doc = JsonConvert.DeserializeObject<DocumentoSolicitudAdmin>(solicitud.DocSolicitud);

            if (solicitud.CodigoEstado == "RE")
            {
                var TipoSolicitud = _context.TipoSolicitud
                                         .Where(s => s.NOMBRE == "Solicitud de vacaciones")
                                         .FirstOrDefault();

                if (idTipoSolicitud != TipoSolicitud.IDTIPOSOLICITUD)
                {
                    return true;
                }

                var TipoSolicitudPermisos = _context.TipoSolicitud
                                .Where(s => s.NOMBRE == "Solicitud de permiso")
                                 .FirstOrDefault();


                var solicitudesAprobadasDoc = _context.SolicitudMasterData
                                            .Where(s =>
                                                        s.IDTIPOSOLICITUD == TipoSolicitudPermisos.IDTIPOSOLICITUD
                                                        && s.IDCOLABORADOR == solicitud.IdColaborador
                                                        && estadosEnTransito.Contains(s.ESTADOSOLICITUD)).ToList();

                var resultados = solicitudesAprobadasDoc.Select(s => JsonConvert.DeserializeObject<DocumentoSolicitudAdmin>(s.DOCSOLICITUD)).Where(s => s.FechaInicio <= doc.FechaFinal && s.FechaFinal >= doc.FechaInicio).ToList();




                if (resultados.Count > 0)
                {
                    return false;

                }

                return true;
            }
            else
            {
                return true;
            }



        }

        private bool SolicitudAdminPermisosTraslapeEnVacaciones(WorkflowCommand solicitud, int idTipoSolicitud)
        {
            List<string> estadosEnTransito = new List<string>() { "AP", "EPV", "EP", "EA", "CO" };

            DocumentoSolicitudAdmin doc;

            doc = JsonConvert.DeserializeObject<DocumentoSolicitudAdmin>(solicitud.DocSolicitud);

            if (solicitud.CodigoEstado == "RE")
            {
                var TipoSolicitud = _context.TipoSolicitud
                                .Where(s => s.NOMBRE == "Solicitud de permiso")
                                .FirstOrDefault();

                if (idTipoSolicitud != TipoSolicitud.IDTIPOSOLICITUD)
                {
                    return true;
                }

                var TipoSolicitudVacaciones = _context.TipoSolicitud
                                 .Where(s => s.NOMBRE == "Solicitud de vacaciones")
                                 .FirstOrDefault();


                var solicitudesAprobadasDoc = _context.SolicitudMasterData
                                            .Where(s =>
                                                        s.IDTIPOSOLICITUD == TipoSolicitudVacaciones.IDTIPOSOLICITUD
                                                        && s.IDCOLABORADOR == solicitud.IdColaborador
                                                        && estadosEnTransito.Contains(s.ESTADOSOLICITUD)).ToList();

                var resultados = solicitudesAprobadasDoc.Select(s => JsonConvert.DeserializeObject<DocumentoSolicitudAdmin>(s.DOCSOLICITUD)).Where(s => s.FechaInicio <= doc.FechaFinal && s.FechaFinal >= doc.FechaInicio).ToList();




                if (resultados.Count > 0)
                {
                    return false;

                }

                return true;
            }
            else
            {
                return true;
            }



        }





        private bool SolicitudAdminPermisosTraslapeEnIncapacidades(WorkflowCommand solicitud, int idTipoSolicitud)
        {
            List<string> estadosEnTransito = new List<string>() { "AP", "EPV", "EP", "EA", "CO" };

            DocumentoSolicitudAdmin doc;

            doc = JsonConvert.DeserializeObject<DocumentoSolicitudAdmin>(solicitud.DocSolicitud);

            if (solicitud.CodigoEstado == "RE")
            {
                var TipoSolicitud = _context.TipoSolicitud
                                .Where(s => s.NOMBRE == "Solicitud de permiso")
                                .FirstOrDefault();

                if (idTipoSolicitud != TipoSolicitud.IDTIPOSOLICITUD)
                {
                    return true;
                }

                var TipoSolicitudIncapacidades = _context.TipoSolicitud
                                 .Where(s => s.NOMBRE == "Registro de incapacidades")
                                 .FirstOrDefault();


                var solicitudesAprobadasDoc = _context.SolicitudMasterData
                                            .Where(s =>
                                                        s.IDTIPOSOLICITUD == TipoSolicitudIncapacidades.IDTIPOSOLICITUD
                                                        && s.IDCOLABORADOR == solicitud.IdColaborador
                                                        && estadosEnTransito.Contains(s.ESTADOSOLICITUD)).ToList();

                var resultados = solicitudesAprobadasDoc.Select(s => JsonConvert.DeserializeObject<DocumentoSolicitudAdmin>(s.DOCSOLICITUD)).Where(s => s.FechaInicio <= doc.FechaFinal && s.FechaFinal >= doc.FechaInicio).ToList();




                if (resultados.Count > 0)
                {
                    return false;

                }

                return true;
            }
            else
            {
                return true;
            }



        }

        private bool SolicitudAdminVacacionesTraslapeEnIncapacidades(WorkflowCommand solicitud, int idTipoSolicitud)
        {
            List<string> estadosEnTransito = new List<string>() { "AP", "EPV", "EP", "EA", "CO" };

            DocumentoSolicitudAdmin doc;

            doc = JsonConvert.DeserializeObject<DocumentoSolicitudAdmin>(solicitud.DocSolicitud);

            if (solicitud.CodigoEstado == "RE")
            {
                var TipoSolicitud = _context.TipoSolicitud
                                         .Where(s => s.NOMBRE == "Solicitud de vacaciones")
                                         .FirstOrDefault();

                if (idTipoSolicitud != TipoSolicitud.IDTIPOSOLICITUD)
                {
                    return true;
                }

                var TipoSolicitudIncapacidades = _context.TipoSolicitud
                                  .Where(s => s.NOMBRE == "Registro de incapacidades")
                                 .FirstOrDefault();


                var solicitudesAprobadasDoc = _context.SolicitudMasterData
                                            .Where(s =>
                                                        s.IDTIPOSOLICITUD == TipoSolicitudIncapacidades.IDTIPOSOLICITUD
                                                        && s.IDCOLABORADOR == solicitud.IdColaborador
                                                        && estadosEnTransito.Contains(s.ESTADOSOLICITUD)).ToList();

                var resultados = solicitudesAprobadasDoc.Select(s => JsonConvert.DeserializeObject<DocumentoSolicitudAdmin>(s.DOCSOLICITUD)).Where(s => s.FechaInicio <= doc.FechaFinal && s.FechaFinal >= doc.FechaInicio).ToList();




                if (resultados.Count > 0)
                {
                    return false;

                }

                return true;
            }
            else
            {
                return true;
            }



        }


        private bool SolicitudIdiomasNivelExistente(WorkflowCommand solicitud, int idTipoSolicitud)
        {




            if (solicitud.CodigoEstado == "RE")
            {
                var TipoSolicitud = _context.TipoSolicitud
                                         .Where(s => s.NOMBRE == "Idiomas (Nivel de idioma)")
                                         .FirstOrDefault();

                DocumentoSolicitudIdiomas doc;

                doc = JsonConvert.DeserializeObject<DocumentoSolicitudIdiomas>(solicitud.DocSolicitud);


                if (idTipoSolicitud != TipoSolicitud.IDTIPOSOLICITUD)
                {
                    return true;
                }

                Task<List<IdiomaColaboradorDto>> resultados = _context.ConsultaIdiomasColaboradores(JsonConvert.SerializeObject(doc.nivelIdiomasList.Where(i => i.estado == "N")));
                resultados.Wait();
                if (resultados.Result.Count > 0)
                {
                    return false;

                }


            }


            return true;
        }

    }
}
