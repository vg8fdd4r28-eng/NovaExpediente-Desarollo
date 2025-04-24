using MediatR;

namespace NovaExpediente.Application.CentrosEstudios.Queries.GetCentroEstudioDetalle
{
    public class GetCentroEstudioDetalleListQuery : IRequest<CentroEstudioDetalleVm>
    {
        public int Id { get; set; }
    }
}
