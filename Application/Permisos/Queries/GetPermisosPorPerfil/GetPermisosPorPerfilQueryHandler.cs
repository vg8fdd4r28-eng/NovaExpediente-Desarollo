using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Colaboradores.Queries.GetDetalleColaboradorResumen;
using NovaExpediente.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Permisos.Queries.GetPermisosPorPerfil
{
    public class GetPermisosPorPerfilQueryHandler : IRequestHandler<GetPermisosPorPerfilQuery, PermisoPorPerfilVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetPermisosPorPerfilQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PermisoPorPerfilVm> Handle(GetPermisosPorPerfilQuery request, CancellationToken cancellationToken)
        {
            PermisoPorPerfilVm vm = new PermisoPorPerfilVm();
            List<PermisoPerfilDto> permisos = new List<PermisoPerfilDto>();

            // si es Expediente.RRHH o Financiero todas los Permisos
            if (request.Roles.Contains("Expediente.RRHH") || request.Roles.Contains("Expediente.Financiero"))
            {
                permisos = await _context.Permisos
                                        .Where(v => (v.FECHAINICIO >= request.FechaInicio && v.FECHAINICIO <= request.FechaFin) ||
                                                    (v.FECHAFINAL >= request.FechaInicio && v.FECHAFINAL <= request.FechaFin))
                                        .Include(c => c.Colaborador)
                                        .Include(c => c.TipoPermisoGoce)
                                        .Include(c => c.SolicitudMasterData)
                                        .ProjectTo<PermisoPerfilDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken);
            }
            // sino, si es Expediente.DM  los Permisos a cargo de REQUEST.IDCOLABORADOR y los propios
            else if (request.Roles.Contains("Expediente.DM"))
            {
              var  colaboradores = await _context.Colaboradores
                                     .Where(c => c.IDDELIVERYMANAGER == request.IdColaborador || c.IDCOLABORADOR == request.IdColaborador)
                                     .ProjectTo<ColaboradorResumenDto>(_mapper.ConfigurationProvider)
                                     .Select(c => c.IDCOLABORADOR)
                                     .ToListAsync(cancellationToken);

                permisos = await _context.Permisos
                                        .Where(c => colaboradores.Contains(c.IDCOLABORADOR) &&
                                                    ((c.FECHAINICIO >= request.FechaInicio && c.FECHAINICIO <= request.FechaFin) ||
                                                    (c.FECHAFINAL >= request.FechaInicio && c.FECHAFINAL <= request.FechaFin)))
                                        .Include(c => c.Colaborador)
                                        .Include(c => c.TipoPermisoGoce)
                                        .Include(c => c.SolicitudMasterData)
                                        .ProjectTo<PermisoPerfilDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken);
            }
            // sino, si es Expediente.Colaborador los Permisos propios
            else if (request.Roles.Contains("Expediente.Colaborador"))
            {
                permisos = await _context.Permisos
                                        .Where(c => c.IDCOLABORADOR == request.IdColaborador &&
                                                    ((c.FECHAINICIO >= request.FechaInicio && c.FECHAINICIO <= request.FechaFin) ||
                                                    (c.FECHAFINAL >= request.FechaInicio && c.FECHAFINAL <= request.FechaFin)))
                                        .Include(c => c.Colaborador)
                                        .Include(c => c.TipoPermisoGoce)
                                        .Include(c => c.SolicitudMasterData)
                                        .ProjectTo<PermisoPerfilDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken);
            }

            vm.Permisos = permisos;
            vm.Count = permisos.Count();

            return vm;
        }
    }
}
