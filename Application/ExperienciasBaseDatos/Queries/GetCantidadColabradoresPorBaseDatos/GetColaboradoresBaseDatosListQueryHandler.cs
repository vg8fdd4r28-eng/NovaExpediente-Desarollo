using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ExperienciasBaseDatos.Queries.GetCantidadColabradoresPorBaseDatos
{
    class GetColaboradoresBaseDatosListQueryHandler : IRequestHandler<GetColaboradoresBaseDatosListQuery, ColaboradoresBaseDatosListVm>
    {
        private readonly INovaExpedienteDbContext _context;


        public GetColaboradoresBaseDatosListQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }


        public async Task<ColaboradoresBaseDatosListVm> Handle(GetColaboradoresBaseDatosListQuery request, CancellationToken cancellationToken)
        {

            var colaboradores = await _context.ExperienciasBaseDatos
                .GroupBy(d => d.BaseDatos.NOMBRE)
                .OrderByDescending(d => d.Count())
                .Select(g => new ColaboradoresBaseDatosDto() { BaseDatos = g.Key.ToString(), CANTIDAD = g.Count() })  
                .Take(5)
                .ToListAsync(cancellationToken);

            var vm = new ColaboradoresBaseDatosListVm
            {
                colaboradores = colaboradores,
                cantidad = colaboradores.Count()
            };

            return vm;
        }
    }
}
