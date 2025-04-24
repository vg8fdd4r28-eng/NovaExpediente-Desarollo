using MediatR;

namespace NovaExpediente.Application.Idiomas.Queries.GetIdiomasDetalle
{
    public class GetIdiomaDetalleListQuery : IRequest<IdiomaDetalleVm>
    {
        public int Id { get; set; }
    }
}
