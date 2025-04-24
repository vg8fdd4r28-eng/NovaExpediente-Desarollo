using MediatR;

namespace NovaExpediente.Application.Certificaciones.Queries.GetCertificacionDetalle
{
    public class GetCertificacionDetalleListQuery : IRequest<CertificacionDetalleVm>
    {
        public int Id { get; set; }
    }
}





