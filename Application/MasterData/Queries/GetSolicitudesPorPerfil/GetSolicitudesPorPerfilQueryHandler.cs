using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetSolicitudesPorPerfil
{
    public class GetSolicitudesPorPerfilQueryQueryHandler : IRequestHandler<GetSolicitudesPorPerfilQuery, SolicitudesPorPerfilVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetSolicitudesPorPerfilQueryQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SolicitudesPorPerfilVm> Handle(GetSolicitudesPorPerfilQuery request, CancellationToken cancellationToken)
        {
            var solicitudes = await _context.TipoSolicitudPerfil
                .Include(p => p.TipoSolicitud)
                .Where(p => p.ESTADOSOLICITUD.Equals("RE") && p.TipoSolicitud.CATEGORIA == (string.IsNullOrEmpty(request.Categoria) ? "MD"  : request.Categoria))
                .ToListAsync(cancellationToken);

            var vm = new SolicitudesPorPerfilVm
            {
                TiposDeSolicitud = solicitudes
                                    .Where(p => request.Roles.Contains(p.NOMBRE_DEL_PERFIL))
                                    .Select(p => new SolicitudPorPerfilDto()
                                    {
                                        IdTipoSolicitud = p.IDTIPOSOLICITUD,
                                        TipoSolicitud = p.TipoSolicitud.NOMBRE
                                    }).Distinct().OrderBy(p => p.TipoSolicitud).ToList()
            };
            vm.TiposDeSolicitud = vm.TiposDeSolicitud
                                      .GroupBy(n => new { n.IdTipoSolicitud, n.TipoSolicitud })
                                      .Select(g => new SolicitudPorPerfilDto
                                      {
                                          IdTipoSolicitud = g.Key.IdTipoSolicitud,
                                          TipoSolicitud = g.Key.TipoSolicitud
                                      })
                                      .ToList();

            return vm;
        }
    }
}
