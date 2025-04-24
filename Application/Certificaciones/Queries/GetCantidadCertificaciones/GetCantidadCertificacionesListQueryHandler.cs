using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Certificaciones.Queries.GetCantidadCertificaciones
{
    class GetCantidadCertificacionesListQueryHandler : IRequestHandler<GetCantidadCertificacionesListQuery, CantidadCertificacionesListVm>
    {

        private readonly INovaExpedienteDbContext _context;

        public GetCantidadCertificacionesListQueryHandler(INovaExpedienteDbContext context)
        {
            _context = context;
        }

        public async Task<CantidadCertificacionesListVm> Handle(GetCantidadCertificacionesListQuery request, CancellationToken cancellationToken)
        {
            var CERTIFICACIONCOLABORADOR = await _context.CertificacionColaboradors

                .Where(c=> (c.CertificacionProveedor.ProveedorCertificacion.Nombre=="ORACLE") || (c.CertificacionProveedor.ProveedorCertificacion.Nombre=="MICROSOFT" ) )
                .GroupBy(d => d.CertificacionProveedor.ProveedorCertificacion.Nombre)
                .Select(g => new CantidadCertificacionesDto() { ProveedorCertificacion= g.Key.ToString(), CANTIDAD = g.Count().ToString() })
                .ToListAsync(cancellationToken);

            var vm = new CantidadCertificacionesListVm
            {
                CERTIFICACIONCOLABORADOR = CERTIFICACIONCOLABORADOR,
                CANTIDAD = CERTIFICACIONCOLABORADOR.Count
            };

            return vm;
        }
    }
}
