using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresGenero
{
    class GetColaboradoresGeneroListQueryHandler : IRequestHandler<GetColaboradoresGeneroListQuery, ColaboradoresGeneroListVm>
    {

        private readonly INovaExpedienteDbContext _context;

        public GetColaboradoresGeneroListQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }
        public async Task<ColaboradoresGeneroListVm> Handle(GetColaboradoresGeneroListQuery request, CancellationToken cancellationToken)
        {

            var colaboradores = await _context.Colaboradores  
                .Where(c => c.ACTIVO )
                .GroupBy(d => d.GENERO)
                .Select(g => new ColaboradoresGeneroDto() { GENERO= g.Key.ToString(), CANTIDAD = g.Count().ToString() })
                .ToListAsync(cancellationToken);
            
            var vm = new ColaboradoresGeneroListVm
            {
                Colaboradores = colaboradores,
                Cantidad = colaboradores.Count()
             };

            return vm;
        }
    }
}
