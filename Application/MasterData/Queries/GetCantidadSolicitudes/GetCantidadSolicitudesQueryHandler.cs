using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetCantidadSolicitudes
{
    public class GetCantidadSolicitudesQueryHandler : IRequestHandler<GetCantidadSolicitudesQuery, CantidadSolicitudesVm>
    {

        private readonly INovaExpedienteDbContext _context;

        public GetCantidadSolicitudesQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }
        public async Task<CantidadSolicitudesVm> Handle(GetCantidadSolicitudesQuery request, CancellationToken cancellationToken)
        {
            //List<string> estadosEnTransito = new List<string>()
            //    {
            //        "EV", "CO", "EA"
            //    };
            //var cantidadSolicitudes = await _context.SolicitudMasterData.CountAsync();
            //var enTransito = await _context.SolicitudMasterData
            //                        .Where(c => estadosEnTransito.Contains(c.ESTADOSOLICITUD)
            //                                    && c.TipoSolicitud.CATEGORIA == (string.IsNullOrEmpty(request.Categoria) ? "MD" : request.Categoria))
            //                        .ToListAsync(cancellationToken);

            Dictionary<string, int> resultado = await _context.ObtenerConteoSolicitudes();

            var vm = new CantidadSolicitudesVm
            {
                TotalSolicitudes = resultado["TotalSolicitudes"],
                SolicitudesEnTransito = resultado["SolicitudesEnTransito"]
            };

            return vm;
        }
    }
}
