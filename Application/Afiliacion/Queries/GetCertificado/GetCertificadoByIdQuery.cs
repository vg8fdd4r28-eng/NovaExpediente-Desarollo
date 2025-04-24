using MediatR;

namespace NovaExpediente.Application.Afiliacion.Queries.GetCertificado
{
   public class GetCertificadoByIdQuery : IRequest<CertificadoAfiliacionVm>
    {
        public int Id { get; set; }
    }
}





