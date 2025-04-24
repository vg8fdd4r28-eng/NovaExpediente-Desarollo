using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Application.Notifications.Models;
using NovaExpediente.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;



namespace NovaExpediente.Application.EnvioCorreo.Commands
{
    public class EnvioCorreoCommand : IRequest<string>
    {
        public int IdColaborador { get; set; }
        public TipoNotificacion Tipo { get; set; }


        public class EnvioCorreoCommandHandler : IRequestHandler<EnvioCorreoCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;
            private readonly INotificationService _notificationService;


            public EnvioCorreoCommandHandler(INovaExpedienteDbContext context, INotificationService notificationService)
            {
                _context = context;
                _notificationService = notificationService;

            }

            public async Task<string> Handle(EnvioCorreoCommand request, CancellationToken cancellationToken)
            {
                string result = "";

                if (request.IdColaborador > 0)
                {
                    await Notificar(request);
                }
                else
                {
                    throw new Exception("Id de colaborador inválido");
                }

                return result;

            }



            private async Task Notificar(EnvioCorreoCommand request)
            {
                MensajeSolicitudAdministrativa mensaje;
                Colaborador solicitante;
                solicitante = _context.Colaboradores
                                 .Where(p => p.IDCOLABORADOR == request.IdColaborador)
                                 .First();



                string datosSolicitante = $"{solicitante.NOMBRE.Trim()} {solicitante.PRIMERAPELLIDO.Trim()} {solicitante.SEGUNDOAPELLIDO.Trim()}" +
                                                        $" con número de identificación {solicitante.IDENTIFICACION.Trim()}";



                mensaje = new MensajeSolicitudAdministrativa();


                switch (request.Tipo)
                {
                    case TipoNotificacion.ConsultaSaldoVacaciones:
                        mensaje.CorreoResponsable = "riveramirez94@gmail.com";
                        mensaje.NombreResponsable = "Edgar Esquivel Garro";
                        mensaje.Solicitante = datosSolicitante;
                        mensaje.CorreoSolicitante = solicitante.CORREO;
                        mensaje.TipoNotificacion = TipoNotificacion.ConsultaSaldoVacaciones;

                        break;

                    case TipoNotificacion.DenunciaAPP:
                        mensaje.CorreoResponsable = "riveramirez94@gmail.com";
                        mensaje.NombreResponsable = "Sergio Rivera Ramirez";
                        mensaje.Solicitante = datosSolicitante;
                        mensaje.CorreoSolicitante = solicitante.CORREO;
                        mensaje.TipoNotificacion = TipoNotificacion.DenunciaAPP;
                        mensaje.Denuncia = "";

                        break;

                    default:
                        break;
                }

                await _notificationService.SendAsync(mensaje);
            }


        }

    }
}
