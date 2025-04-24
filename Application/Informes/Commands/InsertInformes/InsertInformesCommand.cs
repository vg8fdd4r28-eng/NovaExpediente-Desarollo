using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Informes.Commands.InsertInformes
{
    public class InsertInformesCommand : IRequest<string>
    {
        public string UserId { get; set; }
        public class InsertInformesCommandHandler : IRequestHandler<InsertInformesCommand, string>
        {
            private readonly IInformeService _informeService;

            public InsertInformesCommandHandler(IInformeService informeService) => _informeService = informeService;

            public async Task<string> Handle(InsertInformesCommand request,
                CancellationToken cancellationToken) =>
                await _informeService.CrearConversacion(request.UserId);
        }
    }
}
