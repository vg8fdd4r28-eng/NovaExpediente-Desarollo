using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Application.Notifications.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using NovaExpediente.Application.Common.Models;

namespace NovaExpediente.Infrastructure
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(MessageDto message)
        {
            return Task.CompletedTask;
        }

        private readonly IConfiguration Configuration;
        private readonly ILoggingService _loggingService;

        public NotificationService(IConfiguration configuration, ILoggingService loggingService)
        {
            Configuration = configuration;
            _loggingService = loggingService;      
        }


        public async Task SendAsync(MensajeSolicitudAdministrativa message)
        {
            // key del servicio SENDGRID
            var apiKey = Configuration.GetSection("GeneracionCorreo").GetSection("apiKey").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(Configuration.GetSection("GeneracionCorreo").GetSection("from").Value, "Notificaciones del Expediente Digital");
            var to = new EmailAddress(message.CorreoResponsable, message.NombreResponsable);

            // mensaje a enviar
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, new List<EmailAddress>() { to },
                                                                       "", "", "", true);

            // agregan los correos al CC
            var cc = new List<EmailAddress>();
            if (!string.IsNullOrEmpty(message.CorreoAdicionalNotificar))
            {
                cc.Add(new EmailAddress(message.CorreoAdicionalNotificar));
            }

            if (message.CorreoResponsable != message.CorreoSolicitante)
            {
                cc.Add(new EmailAddress(message.CorreoSolicitante));
            }

            if (cc.Count > 0)
            {
                msg.AddCcs(cc);
            }


            // identificador de la plantilla y los datos requeridos 
            msg.SetTemplateId(GetTemplateId(message.TipoNotificacion, message.Estado));
            msg.SetTemplateData(GetTemplateData(message));

            var result = await client.SendEmailAsync(msg);
            if (result.IsSuccessStatusCode)
            {
                _loggingService.LogInformation(EnumLoggingCategory.Correo, $"SendAsync - {result.Body.ReadAsStringAsync().Result}", message.IdSolicitud);
            }
            else
            {
                _loggingService.LogError(EnumLoggingCategory.Correo, $"SendAsync - {result.Body.ReadAsStringAsync().Result} - {JsonConvert.SerializeObject(message)} ", message.IdSolicitud);
            }
        }

        private string GetTemplateId(TipoNotificacion tipoNotificacion, string estado)
        {
            string templateId = string.Empty;
            switch (tipoNotificacion)
            {
                case TipoNotificacion.ConstanciaSalario:
                    templateId = Configuration.GetSection("GeneracionCorreo").GetSection("IdPlantillaConstanciaSalario").Value;
                    break;
                case TipoNotificacion.RestriccionVehicular:
                    templateId = Configuration.GetSection("GeneracionCorreo").GetSection("IdPlantillaRestriccionVehicular").Value;
                    break;
                case TipoNotificacion.RenovacionDIMEX:
                    templateId = Configuration.GetSection("GeneracionCorreo").GetSection("IdPlantillaRenovacionDimex").Value;
                    break;
                case TipoNotificacion.ServiciosLaborales:
                    templateId = Configuration.GetSection("GeneracionCorreo").GetSection("IdPlantillServiciosLaborales").Value;
                    break;
                case TipoNotificacion.Vacaciones:
                    if (estado == "EPV")
                    {
                        templateId = Configuration.GetSection("GeneracionCorreo").GetSection("IdPlantillaVacacionesDM").Value;
                    }
                    else if (estado == "EA")
                    {
                        templateId = Configuration.GetSection("GeneracionCorreo").GetSection("IdPlantillaVacacionesRRHH").Value;
                    }
                    else if (estado == "CO")
                    {
                        templateId = Configuration.GetSection("GeneracionCorreo").GetSection("IdPlantillaVacacionesCorregir").Value;
                    }
                    else if (estado == "PE" || estado == "DE")
                    {
                        templateId = Configuration.GetSection("GeneracionCorreo").GetSection("IdPlantillaGeneralResultadoAprobacion").Value;
                    }
                    break;
                case TipoNotificacion.ReasignarResponsable:
                    templateId = Configuration.GetSection("GeneracionCorreo").GetSection("IdPlantillaReasignarResponsable").Value;
                    break;
                case TipoNotificacion.Incapacidades:
                    if (estado == "EA")
                    {
                        templateId = Configuration.GetSection("GeneracionCorreo").GetSection("IdPlantillaIncapacidadesRRHH").Value;
                    }
                    else if (estado == "CO")
                    {
                        templateId = Configuration.GetSection("GeneracionCorreo").GetSection("IdPlantillaIncapacidadesCorregir").Value;
                    }
                    else if (estado == "PE" || estado == "DE")
                    {
                        templateId = Configuration.GetSection("GeneracionCorreo").GetSection("IdPlantillaGeneralResultadoAprobacion").Value;
                    }
                    break;
                case TipoNotificacion.Permisos:
                    if (estado == "EPV")
                    {
                        templateId = Configuration.GetSection("GeneracionCorreo").GetSection("IdPlantillaPermisosDM").Value;
                    }
                    else if (estado == "EA")
                    {
                        templateId = Configuration.GetSection("GeneracionCorreo").GetSection("IdPlantillaPermisosRRHH").Value;
                    }
                    else if (estado == "CO")
                    {
                        templateId = Configuration.GetSection("GeneracionCorreo").GetSection("IdPlantillaPermisosCorregir").Value;
                    }
                    else if (estado == "PE" || estado == "DE")
                    {
                        templateId = Configuration.GetSection("GeneracionCorreo").GetSection("IdPlantillaGeneralResultadoAprobacion").Value;
                    }
                    break;
                case TipoNotificacion.ConsultaSaldoVacaciones:
                    templateId = Configuration.GetSection("GeneracionCorreo").GetSection("IdPlantillaSaldoVacaciones").Value;
                    break;
                case TipoNotificacion.DenunciaAPP:
                    templateId = Configuration.GetSection("GeneracionCorreo").GetSection("IdPlantillaDenunciasAPP").Value;
                    break;
                default:
                    break;
            }

            return templateId;
        }
        private object GetTemplateData(MensajeSolicitudAdministrativa message)
        {
            var url_expediente = Configuration.GetSection("GeneracionCorreo").GetSection("url_expediente").Value;
            var url_aprobar = Configuration.GetSection("GeneracionCorreo").GetSection("path_aprobar").Value;
            object templateData = null;

            // se reemplazan los parámetros en la hilera 'Administrative/Redirect?id={0}&e={1}'
            url_aprobar = string.Concat(url_expediente, "/", string.Format(url_aprobar, message.IdSolicitud, message.Estado));

            switch (message.TipoNotificacion)
            {
                case TipoNotificacion.ConstanciaSalario:
                    var definicionConstanciaSalario = new { descripcion = "", estado = "" };
                    var solicitudConstanciaSalario = JsonConvert.DeserializeAnonymousType(message.DocSolicitud, definicionConstanciaSalario);

                    templateData = new
                    {
                        usuario_responsable = message.NombreResponsable,
                        solicitante = message.Solicitante,
                        observaciones = solicitudConstanciaSalario.descripcion,
                        numero_solicitud = message.IdSolicitud,
                        url_expediente = url_expediente,
                        url_aprobar = url_aprobar
                    };
                    break;
                case TipoNotificacion.RestriccionVehicular:
                    var definicionRestriccionVehicular = new { numeroPlaca = "", direccionDestino = "", direccionOrigen = "", descripcion = "", estado = "" };
                    var solicitudRestriccionVehicular = JsonConvert.DeserializeAnonymousType(message.DocSolicitud, definicionRestriccionVehicular);

                    templateData = new
                    {
                        usuario_responsable = message.NombreResponsable,
                        solicitante = message.Solicitante,
                        observaciones = solicitudRestriccionVehicular.descripcion,
                        numero_solicitud = message.IdSolicitud,
                        numeroPlaca = solicitudRestriccionVehicular.numeroPlaca,
                        direccionOrigen = solicitudRestriccionVehicular.direccionOrigen,
                        direccionDestino = solicitudRestriccionVehicular.direccionDestino,
                        url_expediente = url_expediente,
                        url_aprobar = url_aprobar
                    };
                    break;

                case TipoNotificacion.RenovacionDIMEX:
                    var definicionRenovacionDIMEX = new { descripcion = "", estado = "" };
                    var solicitudRenovacionDIMEX = JsonConvert.DeserializeAnonymousType(message.DocSolicitud, definicionRenovacionDIMEX);

                    templateData = new
                    {
                        usuario_responsable = message.NombreResponsable,
                        solicitante = message.Solicitante,
                        observaciones = solicitudRenovacionDIMEX.descripcion,
                        numero_solicitud = message.IdSolicitud,
                        url_expediente = url_expediente,
                        url_aprobar = url_aprobar
                    };
                    break;
                case TipoNotificacion.ServiciosLaborales:
                    var definicionServiciosLaborales = new { descripcion = "", estado = "" };
                    var solicitudServiciosLaborales = JsonConvert.DeserializeAnonymousType(message.DocSolicitud, definicionServiciosLaborales);

                    templateData = new
                    {
                        usuario_responsable = message.NombreResponsable,
                        solicitante = message.Solicitante,
                        observaciones = solicitudServiciosLaborales.descripcion,
                        numero_solicitud = message.IdSolicitud,
                        url_expediente = url_expediente,
                        url_aprobar = url_aprobar
                    };

                    break;
                case TipoNotificacion.Vacaciones:

                    var definicionVacaciones = new { fechaInicio = "", horaInicio = "", fechaFinal = "", horaFinal = "", descripcion = "", estado = "", cantDias = "" };
                    var solicitudVacaciones = JsonConvert.DeserializeAnonymousType(message.DocSolicitud, definicionVacaciones);

                    templateData = new
                    {
                        usuario_responsable = message.NombreResponsable,
                        solicitante = message.Solicitante,
                        observaciones = solicitudVacaciones.descripcion,
                        numero_solicitud = message.IdSolicitud,
                        fechaInicio = solicitudVacaciones.fechaInicio,
                        fechaFinal = solicitudVacaciones.fechaFinal,
                        delivery_manager = message.NombreResponsableActual,
                        dias = solicitudVacaciones.cantDias,
                        observaciones_aprobador = message.ObservacionesAprobador.Trim() == "" ? "Sin observaciones" : message.ObservacionesAprobador,
                        aprobador = message.NombreResponsableActual,
                        url_expediente = url_expediente,
                        url_aprobar = url_aprobar,

                        accion = this.SetTipoAccion(message.Accion),
                        titulo = "Resultado de aprobación de solicitud",
                        tipoSolicitud = "Solicitud de vacaciones",
                        solicitanteAprobacion = message.SolicitanteAprobacion
                    };
                    break;
                case TipoNotificacion.ReasignarResponsable:
                    templateData = Configuration.GetSection("GeneracionCorreo").GetSection("IdPlantillaReasignarResponsable").Value;

                    break;
                case TipoNotificacion.Incapacidades:

                    var definicionIncapacidades = new { fechaInicio = "", horaInicio = "", fechaFinal = "", horaFinal = "", descripcion = "", estado = "", cantDias = "", numboleta = "", boleta = "", tipoincapacidad = "" };
                    var solicitudIncapacidades = JsonConvert.DeserializeAnonymousType(message.DocSolicitud, definicionIncapacidades);

                    templateData = new
                    {
                        usuario_responsable = message.NombreResponsable,
                        solicitante = message.Solicitante,
                        observaciones = solicitudIncapacidades.descripcion,
                        numero_solicitud = message.IdSolicitud,
                        fechaInicio = solicitudIncapacidades.fechaInicio,
                        fechaFinal = solicitudIncapacidades.fechaFinal,
                        dias = solicitudIncapacidades.cantDias,
                        boleta = solicitudIncapacidades.numboleta,
                        tipoincapacidad = this.SetTipoIncapacidad(solicitudIncapacidades.tipoincapacidad),

                        delivery_manager = message.NombreResponsableActual,
                        numboleta = solicitudIncapacidades.numboleta,
                        observaciones_aprobador = message.ObservacionesAprobador.Trim() == "" ? "Sin observaciones" : message.ObservacionesAprobador,
                        aprobador = message.NombreResponsableActual,
                        url_expediente = url_expediente,
                        url_aprobar = url_aprobar,

                        accion = this.SetTipoAccion(message.Accion),
                        titulo = "Resultado de aprobación de solicitud",
                        tipoSolicitud = "Solicitud de incapacidades",
                        solicitanteAprobacion = message.SolicitanteAprobacion
                    };
                    break;

                case TipoNotificacion.Permisos:

                    var definicionPermisos = new { fechaInicio = "", fechaFinal = "", descripcion = "", estado = "", cantDias = "", Goce = "", Tipo = "", TipoNombre = "" };
                    var solicitudPermisos = JsonConvert.DeserializeAnonymousType(message.DocSolicitud, definicionPermisos);

                    templateData = new
                    {
                        usuario_responsable = message.NombreResponsable,
                        solicitante = message.Solicitante,
                        observaciones = solicitudPermisos.descripcion,
                        numero_solicitud = message.IdSolicitud,
                        fechaInicio = solicitudPermisos.fechaInicio,
                        fechaFinal = solicitudPermisos.fechaFinal,
                        delivery_manager = message.NombreResponsableActual,
                        dias = solicitudPermisos.cantDias,
                        tipopermiso = this.SetTipoPermiso(solicitudPermisos.Goce, solicitudPermisos.TipoNombre),
                        tipo = solicitudPermisos.Tipo,
                        observaciones_aprobador = message.ObservacionesAprobador.Trim() == "" ? "Sin observaciones" : message.ObservacionesAprobador,
                        aprobador = message.NombreResponsableActual,
                        url_expediente = url_expediente,
                        url_aprobar = url_aprobar,
                        accion = this.SetTipoAccion(message.Accion),
                        titulo = "Resultado de aprobación de solicitud",
                        tipoSolicitud = "Solicitud de permisos",
                        solicitanteAprobacion = message.SolicitanteAprobacion

                    };
                    break;

                case TipoNotificacion.ConsultaSaldoVacaciones:
           
                    templateData = new
                    {
                        usuario_responsable = message.NombreResponsable,
                        solicitante = message.Solicitante,
                    };
                    break;

                case TipoNotificacion.DenunciaAPP:

                    templateData = new
                    {
                        usuario_responsable = message.NombreResponsable,
                        solicitante = message.Solicitante,
                        denuncia = message.Denuncia,
                        descripciondenunciatipo= message.DescripcionDenunciaTipo,
                        denunciado= message.Denunciado
                    };
                    break;

                default:
                    break;
            }

            return templateData;
        }



        private string SetTipoIncapacidad(string tipo)
        {
            string res = "";
            switch (tipo)
            {

                case "E":
                    res = "Enfermedad";
                    break;
                case "M":
                    res = "Maternidad";
                    break;
                default:
                    res = "Sin Definir";

                    break;
            }

            return res;


        }

        private string SetTipoPermiso(string tipo, string tipoNombre)
        {
            string res = "";
            switch (tipo)
            {

                case "No":
                    res = "Permiso sin goce de salario ";
                    break;
                case "Si":
                    res = "Permiso con goce de salario. (" + tipoNombre + ")";
                    break;
                default:
                    res = "Sin Definir";

                    break;
            }

            return res;


        }

        private string SetTipoAccion(string tipo)
        {
            string res = "";
            switch (tipo)
            {

                case "Denegar":
                    res = "denegada";
                    break;
                case "Aprobar":
                    res = "aprobada";
                    break;
                default:
                    res = "Sin Definir";
                    break;
            }

            return res;


        }

    }
}
