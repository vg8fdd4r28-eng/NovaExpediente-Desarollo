using MediatR;
namespace NovaExpediente.Application.Informes.Queries.GetListaInformes
{
    public class GetListaInformesQuery : IRequest<string>
    {
        public string UserId { get; set; }
        public int CurrentPage { get; set; }
    }
}
