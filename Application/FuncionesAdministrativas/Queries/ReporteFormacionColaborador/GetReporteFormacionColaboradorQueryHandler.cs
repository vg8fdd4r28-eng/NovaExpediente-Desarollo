using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.FuncionesAdministrativas.Queries.ReporteFormacionColaborador
{
    public class GetReporteFormacionColaboradorQueryHandler : IRequestHandler<GetReporteFormacionColaboradorQuery, string>
    {
        private readonly INovaExpedienteDbContext _context;
        public GetReporteFormacionColaboradorQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }
        public async Task<string> Handle(GetReporteFormacionColaboradorQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.ReporteFormacionColaborador();
            return list;
        }
    }
}
