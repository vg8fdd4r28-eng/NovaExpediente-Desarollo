using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.FuncionesAdministrativas.Queries.ReporteCursosColaborador
{
    public class GetReporteCursosColaboradorQueryHandler : IRequestHandler<GetReporteCursosColaboradorQuery, string>
    {
        private readonly INovaExpedienteDbContext _context;
        public GetReporteCursosColaboradorQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }
        public async Task<string> Handle(GetReporteCursosColaboradorQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.ReporteCursosColaborador();
            return list;
        }
    }
}
