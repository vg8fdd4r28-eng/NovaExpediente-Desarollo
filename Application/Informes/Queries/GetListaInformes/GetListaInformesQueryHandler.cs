using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Informes.Queries.GetListaInformes
{
    public class GetListaInformesQueryHandler : IRequestHandler<GetListaInformesQuery, string>
    {
        private readonly IInformeService _informeService;

        public GetListaInformesQueryHandler(IInformeService informeService) => _informeService = informeService;

        public async Task<string> Handle(GetListaInformesQuery request, CancellationToken cancellationToken) =>
            await _informeService.ConsultaConversaciones(request.UserId, request.CurrentPage)
                .ConfigureAwait(false);
    }
}