using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresEspecialidad
{
    class GetColaboradoresEspecialidadListQueryHandler : IRequestHandler<GetColaboradoresEspecialidadListQuery, ColaboradoresEspecialidadListVm>
    {

        private readonly INovaExpedienteDbContext _context;

        public GetColaboradoresEspecialidadListQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }

        public async Task<ColaboradoresEspecialidadListVm> Handle(GetColaboradoresEspecialidadListQuery request, CancellationToken cancellationToken)
        {

            var colaboradores = await _context.Colaboradores
                .Where(d => ! string.IsNullOrEmpty(d.RAMAESPECIALIDAD))
                .GroupBy(d => d.RAMAESPECIALIDAD)
                .OrderByDescending(d => d.Count())
                .Select(g => new ColaboradoresEspecialidadDto() { RAMAESPECIALIDAD = g.Key.ToString(), CANTIDAD = g.Count() })
                .Take(10)
                .ToListAsync(cancellationToken);

            var vm = new ColaboradoresEspecialidadListVm
            {
                Colaboradores = colaboradores,
                Cantidad = colaboradores.Count()
            };

            return vm;
        }



    }
}
