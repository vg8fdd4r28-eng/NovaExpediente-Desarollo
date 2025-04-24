using MediatR;
using System.Threading.Tasks;
using System.Threading;
using NovaExpediente.Application.Common.Interfaces;

namespace NovaExpediente.Application.Conversaciones.Queries.GetListaConversaciones
{
    public class GetListaConversacionesQueryHandler : IRequestHandler<GetListaConversacionesQuery, string>
    {
        private readonly IConversacionService _service;
        public GetListaConversacionesQueryHandler(IConversacionService service)
        {
            _service = service;
        }

        public async Task<string> Handle(GetListaConversacionesQuery request, CancellationToken cancellationToken)
        {
            string response = await _service.ConsultaConversaciones(request.userId,request.currentPage);
            return response;
        }

    }
}
