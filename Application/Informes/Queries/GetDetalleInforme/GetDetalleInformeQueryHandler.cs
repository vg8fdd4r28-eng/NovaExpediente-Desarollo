using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Informes.Queries.GetDetalleInforme
{
    public class GetDetalleInformeQueryHandler : IRequestHandler<GetDetalleInformeQuery, string>
    {
        private readonly IInformeService _informeService;

        public GetDetalleInformeQueryHandler(IInformeService informeService) => _informeService = informeService;

        public async Task<string> Handle(GetDetalleInformeQuery request, CancellationToken cancellationToken) =>
            await _informeService.ConsultaConversacionDetalle(request.ConversationId)
                .ConfigureAwait(false);
    }
}
