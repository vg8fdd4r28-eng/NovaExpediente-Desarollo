using MediatR;

namespace NovaExpediente.Application.TiposCore.Queries.GetTipoCoreDetalle
{
    public class GetTipoCoreDetalleListQuery : IRequest<TipoCoreDetalleVm>
    {
        public int Id { get; set; }
    }
}
