using MediatR;
using Newtonsoft.Json;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Application.MasterData.Commands.UpsertSolicitudes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NovaExpediente.Application.Common.Models;

namespace NovaExpediente.Application.FuncionesAdministrativas.Commands.RegistroColaborador
{
    public class RegistroColaboradorCommand : IRequest<string>
    {
        public string data { get; set; }


        public class RegistroColaboradorCommandHandler : IRequestHandler<RegistroColaboradorCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;
            private readonly IFireForgetCommandHandler _fireForgetCommand;
          //  private readonly IEventBus _eventBus;

            //  private readonly ISecurityService _securityService;
            //  private readonly INotificationService _notificationService;

            public RegistroColaboradorCommandHandler(INovaExpedienteDbContext context, IFireForgetCommandHandler fireForgetCommand)//, IEventBus eventBus) //, INotificationService notificationService)
            {
                _context = context;
                _fireForgetCommand = fireForgetCommand;
              //  _eventBus = eventBus;
                //   _securityService = securityService;
                // _notificationService = notificationService;
            }

            public async Task<string> Handle(RegistroColaboradorCommand request, CancellationToken cancellationToken)
            {

                // se registra en la base de datos el nuevo colaborador
                Dictionary<string, object> result = await _context.RegistrarColaborador(request.data);

                //// se obtuvo el ID del nuevo colaborador?
                //if ((int)result["idColaborador"] != -1)
                //{
                //    var datos = JsonConvert.DeserializeObject<Dictionary<string, object>>(request.data);
                //    datos["IdColaborador"] = (int)result["idColaborador"];

                //    // se publica evento en un Service Bus para que los suscriptores procesen los datos
                //    NuevoColaboradorIntegrationEvent evento = new NuevoColaboradorIntegrationEvent(datos);
                //    _eventBus.Publish(evento);
                //}

                return result["resultado"].ToString();
            }







        }
    }
}
