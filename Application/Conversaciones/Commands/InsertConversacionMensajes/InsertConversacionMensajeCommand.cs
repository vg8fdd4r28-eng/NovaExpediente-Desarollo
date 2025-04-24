using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading.Tasks;
using System.Threading;

namespace NovaExpediente.Application.Conversaciones.Commands.InsertConversacionMensajes
{
    public class InsertConversacionMensajeCommand : IRequest<string>
    {
        public string conversationId { get; set; }
        public string userId { get; set; }
        public string message { get; set; }
        public class InsertConversacionMensajeCommandHandler : IRequestHandler<InsertConversacionMensajeCommand, string>
        {
            private readonly IConversacionService _service;

            public InsertConversacionMensajeCommandHandler(IConversacionService service)
            {
                _service = service;
            }

            public async Task<string> Handle(InsertConversacionMensajeCommand request, CancellationToken cancellationToken)
            {
                string response = await _service.CrearMensaje(request.userId, request.conversationId, request.message);
                return response;
            }
        }
    }
}
