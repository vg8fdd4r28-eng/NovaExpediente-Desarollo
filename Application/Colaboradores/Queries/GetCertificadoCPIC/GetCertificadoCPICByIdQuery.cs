using MediatR;

namespace NovaExpediente.Application.Colaboradores.Queries.GetCertificadoCPIC
{
    public class GetCertificadoCPICByIdQuery : IRequest<CertificadoCPICVm>
    {
        public int Id { get; set; }
    }
}
