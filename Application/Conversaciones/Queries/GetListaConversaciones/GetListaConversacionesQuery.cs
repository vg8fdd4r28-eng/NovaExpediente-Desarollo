using MediatR;

namespace NovaExpediente.Application.Conversaciones.Queries.GetListaConversaciones
{
    public class GetListaConversacionesQuery : IRequest<string>
    {
        public string userId { get; set; }
        public int currentPage { get; set; }
    }
}
