using MediatR;

namespace NovaExpediente.Application.AplicacionMovil.Sorteos.Queries.GetDetalleSorteo
{
    public class GetDetalleSorteoQuery : IRequest<string>
    {
        public int IdSorteo { get; set; }
    }
}
