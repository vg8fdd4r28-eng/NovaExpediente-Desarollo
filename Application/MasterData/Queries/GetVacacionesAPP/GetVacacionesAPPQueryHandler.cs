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

namespace NovaExpediente.Application.MasterData.Queries.GetVacacionesAPP
{
    public class GetVacacionesAPPQueryHandler : IRequestHandler<GetVacacionesAPPQuery, VacacionesAPPVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetVacacionesAPPQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VacacionesAPPVm> Handle(GetVacacionesAPPQuery request, CancellationToken cancellationToken)
        {
            VacacionesAPPVm vm = new VacacionesAPPVm();
            List<VacacionAPPDto> vacaciones = new List<VacacionAPPDto>();



                vacaciones = await _context.Vacacions
                                        .Where(c => c.IDCOLABORADOR == request.IdColaborador)
                                        .Include(c => c.Colaborador)
                                        .Include(c => c.SolicitudMasterData)
                                        .ProjectTo<VacacionAPPDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken);
            

            vm.Vacaciones = vacaciones;
            vm.Count = vacaciones.Count();

            return vm;
        }
    }
}
