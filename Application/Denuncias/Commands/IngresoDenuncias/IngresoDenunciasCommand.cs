using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using NovaExpediente.Application.EnvioCorreo.Commands;
using NovaExpediente.Application.Notifications.Models;
using NovaExpediente.Domain.Entities;
using System.Linq;
using Newtonsoft.Json;


namespace NovaExpediente.Application.Denuncias.Commands.IngresoDenuncias
{
    public class IngresoDenunciasCommand : IRequest<string>
    {
        public string data { get; set; }


        public class IngresoDenunciasCommandHandler : IRequestHandler<IngresoDenunciasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;
            private readonly IFireForgetCommandHandler _fireForgetCommand;
            private readonly INotificationService _notificationService;


            public IngresoDenunciasCommandHandler(INovaExpedienteDbContext context, IFireForgetCommandHandler fireForgetCommand, INotificationService notificationService)
            {
                _context = context;
                _fireForgetCommand = fireForgetCommand;
                _notificationService = notificationService;

            }

            public async Task<string> Handle(IngresoDenunciasCommand request, CancellationToken cancellationToken)
            {

                // se registra en la base de datos la nueva denuncia
                string result = await _context.IngresarDenunciaAPP(request.data);
                string descripcionDenuncia = "";
                //notifica por correo 
                MensajeSolicitudAdministrativa mensaje = new MensajeSolicitudAdministrativa();


                DataDenuncia dataDenuncia = JsonConvert.DeserializeObject<DataDenuncia>(request.data);

                Colaborador solicitante;
                solicitante = _context.Colaboradores
                                 .Where(p => p.IDCOLABORADOR == dataDenuncia.idDenunciante)
                                 .First();


                switch (dataDenuncia.tipoDenuncia)
                {
                    //Acoso
                    case 1:
                        descripcionDenuncia = "Denuncia por acoso u hostigamiento laboral";
                        break;

                    //Antisoborno
                    case 2:
                        descripcionDenuncia = "Denuncia antisoborno";

                        break;

                    //Salud ocupacional
                    case 3:
                        descripcionDenuncia = "Denuncia de salud ocupacional";

                        break;
                    default:
                        break;
                }

                mensaje.CorreoResponsable = "denuncias@crnova.com";
           //     mensaje.NombreResponsable = "Sergio Rivera Ramirez";
                mensaje.CorreoSolicitante = solicitante.CORREO;
                mensaje.Solicitante = dataDenuncia.nombreDenunciante;
                mensaje.TipoNotificacion = TipoNotificacion.DenunciaAPP;
                mensaje.Denuncia = dataDenuncia.descripcion;
                mensaje.TipoDenuncia = dataDenuncia.tipoDenuncia;
                mensaje.DescripcionDenunciaTipo = descripcionDenuncia;
                mensaje.Denunciado = dataDenuncia.entidadDenunciada  + dataDenuncia.nombreDenunciado;

                await _notificationService.SendAsync(mensaje);

                return result;
            }







        }
    }

    public class DataDenuncia
    {

        public int tipoDenuncia { get; set; }
        public int idDenunciante { get; set; }
        public int esVictima { get; set; }
        public string nombreDenunciante { get; set; }
        public string descripcion { get; set; }
        public string nombreDenunciado { get; set; }
        public string entidadDenunciada { get; set; }

        
            
    }


}
