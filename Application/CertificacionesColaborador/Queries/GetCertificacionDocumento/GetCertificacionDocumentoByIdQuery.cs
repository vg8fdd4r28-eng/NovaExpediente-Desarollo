using MediatR;

namespace NovaExpediente.Application.CertificacionesColaborador.Queries.GetCertificacionDocumento
{
    public class GetCertificacionDocumentoByIdQuery : IRequest<CertificacionDocumentoVm>
    {
        public int Id { get; set; }
    }
}
