using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Colaboradores.Queries.GetColaboradorVacacionesPorPerfil
{
    public class GetColaboradorVacacionesPorPerfilListQueryHandler : IRequestHandler<GetColaboradorVacacionesPorPerfilListQuery, ColaboradorVacacionesPorPerfilVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;


         

        public GetColaboradorVacacionesPorPerfilListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ColaboradorVacacionesPorPerfilVm> Handle(GetColaboradorVacacionesPorPerfilListQuery request, CancellationToken cancellationToken)
        {
            ColaboradorVacacionesPorPerfilVm vm = new ColaboradorVacacionesPorPerfilVm();
            List<ColaboradorVacacionesPorPerfilDto> saldos = new List<ColaboradorVacacionesPorPerfilDto>();

          

            // si es Expediente.RRHH o Financiero todas los saldos de vacaciones
            if (request.Roles.Contains("Expediente.RRHH") || request.Roles.Contains("Expediente.Financiero"))
            {
                saldos = await _context.Colaboradores
                                        .Where(v => v.ACTIVO == true && v.SOLICITAVACACIONES)
                                        .ProjectTo<ColaboradorVacacionesPorPerfilDto>(_mapper.ConfigurationProvider)
                                        .OrderBy(v => v.NombreCompleto)
                                        .ToListAsync(cancellationToken);
            }
            // sino, si es Expediente.DM  los saldos de vacaciones a cargo de REQUEST.IDCOLABORADOR y las propias
            else if (request.Roles.Contains("Expediente.DM"))
            {
                saldos = await _context.Colaboradores
                                      .Where(c => c.ACTIVO == true && c.SOLICITAVACACIONES  && (c.IDDELIVERYMANAGER == request.IdColaborador || c.IDCOLABORADOR == request.IdColaborador))
                                      .ProjectTo<ColaboradorVacacionesPorPerfilDto>(_mapper.ConfigurationProvider)
                                      .OrderBy(v => v.NombreCompleto)
                                      .ToListAsync(cancellationToken);


            }
            // sino, si es Expediente.Colaborador las los saldos de vacaciones propias
            else if (request.Roles.Contains("Expediente.Colaborador"))
            {
                saldos = await _context.Colaboradores
                                      .Where(c => c.ACTIVO == true && c.SOLICITAVACACIONES && c.IDCOLABORADOR == request.IdColaborador)
                                      .ProjectTo<ColaboradorVacacionesPorPerfilDto>(_mapper.ConfigurationProvider)
                                      .OrderBy(v => v.NombreCompleto)
                                      .ToListAsync(cancellationToken);
            }

            vm.ColaboradorVacaciones = saldos;
            vm.Count = saldos.Count();

            return vm;
        }


    }
}
