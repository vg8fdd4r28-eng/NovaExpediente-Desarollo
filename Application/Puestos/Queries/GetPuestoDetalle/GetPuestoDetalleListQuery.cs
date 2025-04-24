using MediatR;

namespace NovaExpediente.Application.Puestos.Queries.GetPuestoDetalle
{
    public class GetPuestoDetalleListQuery : IRequest<PuestoDetalleVm>
    {
        public int Id { get; set; }
    }
}
