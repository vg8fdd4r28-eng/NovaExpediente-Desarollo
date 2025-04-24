using MediatR;

namespace NovaExpediente.Application.BPMS.Queries.GetBPMDetalle
{
    public class GetBPMDetalleListQuery : IRequest<BPMDetalleVm>
    {
        public int Id { get; set; }
    }
}
