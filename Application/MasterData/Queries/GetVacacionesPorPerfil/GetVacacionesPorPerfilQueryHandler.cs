using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Colaboradores.Queries.GetDetalleColaboradorResumen;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetVacacionesPorPerfil
{
    public class GetVacacionesPorPerfilQueryHandler : IRequestHandler<GetVacacionesPorPerfilQuery, VacacionesPorPerfilVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetVacacionesPorPerfilQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VacacionesPorPerfilVm> Handle(GetVacacionesPorPerfilQuery request, CancellationToken cancellationToken)
        {
            VacacionesPorPerfilVm vm = new VacacionesPorPerfilVm();
            List<VacacionPerfilDto> vacaciones = new List<VacacionPerfilDto>();

            // si es Expediente.RRHH o Financiero todas las Vacaciones
            if (request.Roles.Contains("Expediente.RRHH") || request.Roles.Contains("Expediente.Financiero"))
            {
                vacaciones = await _context.Vacacions
                                        .Where(v => (v.FECHAINICIO >= request.FechaInicio && v.FECHAINICIO <= request.FechaFin) ||
                                                    (v.FECHAFINAL >= request.FechaInicio && v.FECHAFINAL <= request.FechaFin))
                                        .Include(c => c.Colaborador)
                                        .Include(c => c.SolicitudMasterData)
                                        .ProjectTo<VacacionPerfilDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken);
            }
            // sino, si es Expediente.DM  las Vacaciones a cargo de REQUEST.IDCOLABORADOR y las propias
            else if (request.Roles.Contains("Expediente.DM"))
            {
              var  colaboradores = await _context.Colaboradores
                                     .Where(c => c.IDDELIVERYMANAGER == request.IdColaborador || c.IDCOLABORADOR == request.IdColaborador)
                                     .ProjectTo<ColaboradorResumenDto>(_mapper.ConfigurationProvider)
                                     .Select(c => c.IDCOLABORADOR)
                                     .ToListAsync(cancellationToken);

                vacaciones = await _context.Vacacions
                                        .Where(c => colaboradores.Contains(c.IDCOLABORADOR) &&
                                                    ((c.FECHAINICIO >= request.FechaInicio && c.FECHAINICIO <= request.FechaFin) ||
                                                    (c.FECHAFINAL >= request.FechaInicio && c.FECHAFINAL <= request.FechaFin)))
                                        .Include(c => c.Colaborador)
                                        .Include(c => c.SolicitudMasterData)
                                        .ProjectTo<VacacionPerfilDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken);
            }
            // sino, si es Expediente.Colaborador las Vacaciones propias
            else if (request.Roles.Contains("Expediente.Colaborador"))
            {
                vacaciones = await _context.Vacacions
                                        .Where(c => c.IDCOLABORADOR == request.IdColaborador && 
                                                    ((c.FECHAINICIO >= request.FechaInicio && c.FECHAINICIO <= request.FechaFin) ||
                                                    (c.FECHAFINAL >= request.FechaInicio && c.FECHAFINAL <= request.FechaFin)))
                                        .Include(c => c.Colaborador)
                                        .Include(c => c.SolicitudMasterData)
                                        .ProjectTo<VacacionPerfilDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken);
            }

            vm.Vacaciones = vacaciones;
            vm.Count = vacaciones.Count();

            return vm;
        }
    }
}
