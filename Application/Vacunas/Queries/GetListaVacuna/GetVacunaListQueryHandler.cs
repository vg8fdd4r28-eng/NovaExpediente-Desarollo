
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.Vacunas.Queries.GetListaVacuna
{
    public class GetVacunaListQueryHandler : IRequestHandler<GetVacunaListQuery, VacunaListVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetVacunaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VacunaListVm> Handle(GetVacunaListQuery request, CancellationToken cancellationToken)
        {


            var _Vacunas = await _context.Vacunas
                .OrderBy(p => p.Nombre)
                .ProjectTo<VacunaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new VacunaListVm
            {
                Vacunas = _Vacunas,
                Count = _Vacunas.Count
            };

            return vm;
        }
    }
}
