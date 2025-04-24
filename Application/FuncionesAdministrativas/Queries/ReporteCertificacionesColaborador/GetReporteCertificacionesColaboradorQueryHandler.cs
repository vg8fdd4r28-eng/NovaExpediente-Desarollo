using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.FuncionesAdministrativas.Queries.ReporteCertificacionesColaborador
{
    public class GetReporteCertificacionesColaboradorQueryHandler : IRequestHandler<GetReporteCertificacionesColaboradorQuery, string>
    {
        private readonly INovaExpedienteDbContext _context;
        public GetReporteCertificacionesColaboradorQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }
        public async Task<string> Handle(GetReporteCertificacionesColaboradorQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.ReporteCertificacionesColaborador();
            return list;
        }
    }
}
