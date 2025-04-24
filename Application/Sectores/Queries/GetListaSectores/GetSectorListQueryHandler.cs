using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Sectores.Queries.GetListaSectores
{
    class GetSectorListQueryHandler : IRequestHandler<GetSectorListQuery, SectorListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetSectorListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SectorListVm> Handle(GetSectorListQuery request, CancellationToken cancellationToken)
        {
            var sector = await _context.Sectors

                .OrderBy(p => p.Nombre)
                .ProjectTo<SectorDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new SectorListVm
            {
                Sectores = sector,
                Count = sector.Count
            };

            return vm;
        }
    }
}
