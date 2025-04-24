using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetSolicitudesEnTransito
{
    public class GetSolicitudesEnTransitoQueryHandler : IRequestHandler<GetSolicitudesEnTransitoQuery, SolicitudesEnTransitoVm>
    {

        private readonly INovaExpedienteDbContext _context;

        public GetSolicitudesEnTransitoQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }

        public async Task<SolicitudesEnTransitoVm> Handle(GetSolicitudesEnTransitoQuery request, CancellationToken cancellationToken)
        {
            List<string> estadosEnTransito = new List<string>()
                {
                    "EV", "CO", "EA"
                };
            var enTransito = await _context.SolicitudMasterData
                                    .Where(c => estadosEnTransito.Contains(c.ESTADOSOLICITUD))
                                    .GroupBy(d => d.ESTADOSOLICITUD)
                                    .Select(g => new SolicitudesEnTransitoDto() { Clasificacion = NombreClasificacion(g.Key.ToString()), Cantidad = g.Count() })
                                    .ToListAsync(cancellationToken);

            var vm = new SolicitudesEnTransitoVm
            {
                Solicitudes = enTransito,
                Cantidad = enTransito.Count()
            };

            return vm;
        }

        private static string NombreClasificacion (string codigoEstado)
        {
            switch (codigoEstado)
            {
                case "EV":
                    return "En validación";
                    break;
                case "CO":
                    return "En correcciones";
                    break;
                case "EA":
                    return "En aprobación";
                    break;
                default:
                    return "";
                    break;
            }
        }
    }
}
