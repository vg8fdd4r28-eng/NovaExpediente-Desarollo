using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Convenios.Queries.GetConvenioDetalleAPP
{
    public class GetConvenioDetalleAPPQueryHandler : IRequestHandler<GetConvenioDetalleAPPQuery, string>
    {
        private readonly INovaExpedienteDbContext _context;

        public GetConvenioDetalleAPPQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(GetConvenioDetalleAPPQuery request, CancellationToken cancellationToken)
        {
            var lista = await _context.ConsultaConvenioDetalleAPP(request.Id);
            return lista;
        }
    }
}