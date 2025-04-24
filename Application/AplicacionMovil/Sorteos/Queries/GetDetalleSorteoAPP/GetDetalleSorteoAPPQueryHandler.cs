using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.AplicacionMovil.Sorteos.Queries.GetDetalleSorteoAPP
{
    public class GetDetalleSorteoAPPQueryHandler : IRequestHandler<GetDetalleSorteoAPPQuery, string>
    {
        private readonly INovaExpedienteDbContext _context;
        public GetDetalleSorteoAPPQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }
        public async Task<string> Handle(GetDetalleSorteoAPPQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.ConsultaDetalleSorteoAPP();
            return list;
        }
    }
}
