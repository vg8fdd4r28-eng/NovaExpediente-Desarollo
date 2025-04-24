using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Convenios.Queries.GetConveniosAPP
{
    public class GetConveniosAPPQueryHandler : IRequestHandler<GetConveniosAPPQuery, string>
    {
        private readonly INovaExpedienteDbContext _context;

        public GetConveniosAPPQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(GetConveniosAPPQuery request, CancellationToken cancellationToken)
        {
            var lista = await _context.ConsultaConveniosAPP(request.IdIdioma, request.IdCategoria);
            return lista;
        }
    }
}