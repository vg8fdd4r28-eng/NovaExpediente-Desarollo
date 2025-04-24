using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetUsoAplicacion
{
    public class GetUsoAplicacionQueryHandler : IRequestHandler<GetUsoAplicacionQuery, UsoAplicacionVm>
    {

        private readonly INovaExpedienteDbContext _context;

        public GetUsoAplicacionQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }
        public async Task<UsoAplicacionVm> Handle(GetUsoAplicacionQuery request, CancellationToken cancellationToken)
        {
            List<object[]> lista = new List<object[]>();

            var listaUso = await _context.UsoAplicacion(request.FechaInicio, request.FechaFin);
            foreach (long key in listaUso.Keys)
            {
                lista.Add(new object[] { key, listaUso[key] });
            }
            var vm = new UsoAplicacionVm
            {
                Series = lista
            };

            return vm;
        }
    }
}
