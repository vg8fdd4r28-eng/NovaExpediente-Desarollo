using MediatR;

namespace NovaExpediente.Application.ProveedorCerficaciones.Queries.GetProveedorCertificacionesDetalle
{
    public class GetProveedorCertificacionesDetalleListQuery : IRequest<ProveedorCertificacionesDetalleVm>
    {
        public int Id { get; set; }
    }
}
