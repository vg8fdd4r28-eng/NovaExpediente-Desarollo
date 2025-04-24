using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Noticias.Queries.GetNoticiasAPP
{
    public class GetNoticiasAPPQueryHandler : IRequestHandler<GetNoticiasAPPQuery, string>
    {

        private readonly INovaExpedienteDbContext _context;

        public GetNoticiasAPPQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }
        public async Task<string> Handle(GetNoticiasAPPQuery request, CancellationToken cancellationToken)
        {


            var lista = await _context.ConsultaNoticiasAPP(request.EsPublica, request.IdIdioma);


            return lista;
        }
    }
}
