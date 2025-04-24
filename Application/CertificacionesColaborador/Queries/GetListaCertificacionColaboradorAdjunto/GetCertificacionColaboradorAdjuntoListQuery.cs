using MediatR;

namespace NovaExpediente.Application.CertificacionesColaborador.Queries.GetListaCertificacionColaboradorAdjunto
{
   public class GetCertificacionColaboradorAdjuntoListQuery : IRequest<CertificacionColaboradorAdjuntoListVm>
    {
        public int Id { get; set; }
    }
}





