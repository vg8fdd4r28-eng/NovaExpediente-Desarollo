using MediatR;

namespace NovaExpediente.Application.MasterDataTipoSolicitud.Queries.GetDetalleTipoSolicitud
{
    public class GetTipoSolicitudDetalleListQuery : IRequest<TipoSolicitudDetalleVm>
    {
        public int Id { get; set; }
    }
}
