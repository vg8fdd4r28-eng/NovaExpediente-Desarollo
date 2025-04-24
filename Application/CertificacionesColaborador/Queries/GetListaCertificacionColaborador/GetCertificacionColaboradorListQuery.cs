using MediatR;

namespace NovaExpediente.Application.CertificacionesColaborador.Queries.GetListaCertificacionColaborador
{
   public class GetCertificacionColaboradorListQuery : IRequest<CertificacionColaboradorListVm>
    {
        public int Id { get; set; }
    }
}





