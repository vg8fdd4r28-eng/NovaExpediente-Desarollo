using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.OneSignalAPP.Commands.SendNotification
{
    public class SendNotificationCommand : IRequest<string>
    {


        public string titulo { get; set; }
        public string msj { get; set; }
        public string[] segmentosStr { get; set; }
        public string todos { get; set; }

        public class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand, string>
        {

            private readonly IOneSignalService _service;
            public SendNotificationCommandHandler(IOneSignalService service)
            {

                _service = service;
            }

            public async Task<string> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
            {

                string res = await _service.SendNotification(request.titulo, request.msj, request.segmentosStr, request.todos);


                return res;

            }
        }


    }



}
