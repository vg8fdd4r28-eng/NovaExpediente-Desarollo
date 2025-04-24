using MediatR;

namespace NovaExpediente.Application.Sectores.Queries.GetSectoresDetalle
{
    public class GetSectorDetalleListQuery : IRequest<SectorDetalleVm>
    {
        public int Id { get; set; }
    }
}
