using MediatR;

namespace NovaExpediente.Application.CertificacionesColaborador.Queries.GetCertificacionColaboradorDetalle
{
   public class GetCertificacionColaboradorDetalleListQuery : IRequest<CertificacionColaboradorDetalleVm>
    {
        public int Id { get; set; }
    }
}





