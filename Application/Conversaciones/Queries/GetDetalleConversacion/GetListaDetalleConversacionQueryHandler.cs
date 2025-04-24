using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading.Tasks;
using System.Threading;

namespace NovaExpediente.Application.Conversaciones.Queries.GetDetalleConversacion
{
    public class GetListaDetalleConversacionQueryHandler : IRequestHandler<GetListaDetalleConversacionQuery, string>
    {
        private readonly IConversacionService _service;
        public GetListaDetalleConversacionQueryHandler(IConversacionService service)
        {
            _service = service;
        }
        public async Task<string> Handle(GetListaDetalleConversacionQuery request, CancellationToken cancellationToken)
        {
            string response = await _service.ConsultaConversacionDetalle(request.conversationId);
            return response;
        }
    }
}
