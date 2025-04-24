using MediatR;

namespace NovaExpediente.Application.Infraestructuras.Queries.GetInfraestructuraDetalle
{
    public class GetInfraestructuraDetalleListQuery : IRequest<InfraestructuraDetalleVm>
    {
        public int Id { get; set; }
    }
}





