using MediatR;
namespace NovaExpediente.Application.Informes.Queries.GetDetalleInforme
{
    public class GetDetalleInformeQuery : IRequest<string>
    {
        public string  ConversationId { get; set; }
    }
}
