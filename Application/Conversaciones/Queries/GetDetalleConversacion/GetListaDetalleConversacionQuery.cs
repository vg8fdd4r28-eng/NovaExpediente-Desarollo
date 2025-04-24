using MediatR;

namespace NovaExpediente.Application.Conversaciones.Queries.GetDetalleConversacion
{
    public class GetListaDetalleConversacionQuery : IRequest<string>
    {
        public string conversationId { get; set; }
    }
}
