using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Vacaciones.Queries.GetListaVacaciones
{
    public class GetVacacionesListQueryHandler : IRequestHandler<GetVacacionesListQuery, VacacionesVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetVacacionesListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VacacionesVm> Handle(GetVacacionesListQuery request, CancellationToken cancellationToken)
        {
            VacacionesVm vm = new VacacionesVm();
            List<VacacionDto> vacaciones = new List<VacacionDto>();

          
                vacaciones = await _context.Vacacions
                                        .Where(c => c.IDSOLICITUDMASTERDATA == request.IdSolicitudMasterData)
                                        .Include(c => c.Colaborador)
                                        .ProjectTo<VacacionDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken);
            
            vm.Vacaciones = vacaciones;
            vm.Count = vacaciones.Count();

            return vm;
        }
    }
}
