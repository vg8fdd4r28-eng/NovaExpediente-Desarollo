using MediatR;

namespace NovaExpediente.Application.CertificacionesProveedor.Queries.GetListaProveedorXCertificacion
{
   public class GetCertificacionProveedorXCertificacionListQuery : IRequest<CertificacionProveedorXCertificacionListVm>
    {
        public int Id { get; set; }
    }
}





