using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Clientes.Queries.GetCantidadClientes
{
    public class GetCantidadClientesQueryHandler : IRequestHandler<GetCantidadClientesQuery, CantidadClientesVm>
    {

        private readonly INovaExpedienteDbContext _context;

        public GetCantidadClientesQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }
        public async Task<CantidadClientesVm> Handle(GetCantidadClientesQuery request, CancellationToken cancellationToken)
        {
            var clientes = await _context.Clientess
                                    .GroupBy(c => c.ACTIVO)
                                    .Select(g => new { ESTADO = g.Key, CANTIDAD = g.Count() })
                                    .ToListAsync(cancellationToken);

            var vm = new CantidadClientesVm
            {
                TotalClientes = clientes.Sum(c => c.CANTIDAD),
                ClientesActivos = clientes.Where(c => c.ESTADO).Select(c => c.CANTIDAD).FirstOrDefault()
            };

            return vm;
        }
    }
}
