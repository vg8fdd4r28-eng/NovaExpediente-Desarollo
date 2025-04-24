using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Informes.Commands.InsertInformeMensaje
{
    public class InsertInformeMensajeCommand : IRequest<string>
    {
        public string ConversationId { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }

        public class InsertInformeMensajeCommandHandler : IRequestHandler<InsertInformeMensajeCommand, string>
        {
            private readonly IInformeService _informeService;

            public InsertInformeMensajeCommandHandler(IInformeService informeService) => _informeService = informeService;

            public async Task<string> Handle(InsertInformeMensajeCommand request,
                CancellationToken cancellationToken) =>
                await _informeService.CrearMensaje(request.UserId, request.ConversationId, request.Message);
        }
    }
}
