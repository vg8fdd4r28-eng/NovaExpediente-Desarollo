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

namespace NovaExpediente.Application.Incapacidades.Queries.GetIncapacidadesPorPerfil
{
    public class GetIncapacidadesPorPerfilQueryHandler : IRequestHandler<GetIncapacidadesPorPerfilQuery, IncapacidadesPorPerfilVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetIncapacidadesPorPerfilQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IncapacidadesPorPerfilVm> Handle(GetIncapacidadesPorPerfilQuery request, CancellationToken cancellationToken)
        {
            IncapacidadesPorPerfilVm vm = new IncapacidadesPorPerfilVm();
            List<IncapacidadPerfilDto> incapacidades = new List<IncapacidadPerfilDto>();

            // si es Expediente.RRHH o Financiero todas las Incapacidades
            if (request.Roles.Contains("Expediente.RRHH") || request.Roles.Contains("Expediente.Financiero"))
            {
                incapacidades = await _context.Incapacidads
                                                    .Where(v => (v.FECHAINICIO >= request.FechaInicio && v.FECHAINICIO <= request.FechaFin) ||
                                                    (v.FECHAFINAL >= request.FechaInicio && v.FECHAFINAL <= request.FechaFin))
                                        .Include(c => c.Colaborador)
                                        .Include(c => c.SolicitudMasterData)
                                        .ProjectTo<IncapacidadPerfilDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken);
            }
            // sino, si es Expediente.DM  las Incapacidades de REQUEST.IDCOLABORADOR y las propias
            else if (request.Roles.Contains("Expediente.DM"))
            {
                var colaboradores = await _context.Colaboradores
                                       .Where(c => c.IDDELIVERYMANAGER == request.IdColaborador || c.IDCOLABORADOR == request.IdColaborador)
                                       .ProjectTo<ColaboradorResumenDto>(_mapper.ConfigurationProvider)
                                       .Select(c => c.IDCOLABORADOR)
                                       .ToListAsync(cancellationToken);

                incapacidades = await _context.Incapacidads
                                             .Where(c => colaboradores.Contains(c.IDCOLABORADOR) &&
                                                    ((c.FECHAINICIO >= request.FechaInicio && c.FECHAINICIO <= request.FechaFin) ||
                                                    (c.FECHAFINAL >= request.FechaInicio && c.FECHAFINAL <= request.FechaFin)))
                                        .Include(c => c.Colaborador)
                                        .Include(c => c.SolicitudMasterData)
                                        .ProjectTo<IncapacidadPerfilDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken);
            }
            // sino, si es Expediente.Colaborador las Incapacidades propias
            else if (request.Roles.Contains("Expediente.Colaborador"))
            {
                incapacidades = await _context.Incapacidads
                                .Where(c => c.IDCOLABORADOR == request.IdColaborador &&
                                                    ((c.FECHAINICIO >= request.FechaInicio && c.FECHAINICIO <= request.FechaFin) ||
                                                    (c.FECHAFINAL >= request.FechaInicio && c.FECHAFINAL <= request.FechaFin)))
                                        .Include(c => c.Colaborador)
                                        .Include(c => c.SolicitudMasterData)
                                        .ProjectTo<IncapacidadPerfilDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken);
            }

            vm.Incapacidades = incapacidades;
            vm.Count = incapacidades.Count();

            return vm;
        }
    }
}
