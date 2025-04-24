using MediatR;

namespace NovaExpediente.Application.HerramientasCalidad.Queries.GetCalidadDetalle
{
    public class GetCalidadDetalleListQuery : IRequest<CalidadDetalleVm>
    {
        public int Id { get; set; }
    }
}





