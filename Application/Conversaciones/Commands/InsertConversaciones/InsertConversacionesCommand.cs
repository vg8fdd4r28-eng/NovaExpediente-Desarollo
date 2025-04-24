using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading.Tasks;
using System.Threading;

namespace NovaExpediente.Application.Conversaciones.Commands.InsertConversaciones
{
    public class InsertConversacionesCommand : IRequest<string>
    {
        public string userId { get; set; }
        public class InsertConversacionesCommandHandler : IRequestHandler<InsertConversacionesCommand, string>
        {
            private readonly IConversacionService _service;

            public InsertConversacionesCommandHandler(IConversacionService service)
            {
                _service = service;
            }

            public async Task<string> Handle(InsertConversacionesCommand request, CancellationToken cancellationToken)
            {
                string response = await _service.CrearConversacion(request.userId);
                return response;
            }
        }
    }
}
