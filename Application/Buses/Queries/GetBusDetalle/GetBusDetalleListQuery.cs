using MediatR;

namespace NovaExpediente.Application.Buses.Queries.GetBusDetalle
{
    public class GetBusDetalleListQuery : IRequest<BusDetalleVm>
    {
        public int Id { get; set; }
    }
}





