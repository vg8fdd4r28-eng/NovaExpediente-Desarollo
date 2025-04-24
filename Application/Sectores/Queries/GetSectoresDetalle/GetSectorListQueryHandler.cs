using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Sectores.Queries.GetSectoresDetalle
{
    class GetSectorDetalleListQueryHandler : IRequestHandler<GetSectorDetalleListQuery, SectorDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetSectorDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SectorDetalleVm> Handle(GetSectorDetalleListQuery request, CancellationToken cancellationToken)
        {
            var sector = await _context.Sectors
                .Where(p => p.IdSector == request.Id)
                .OrderBy(p => p.Nombre)
                .ProjectTo<SectorDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new SectorDetalleVm
            {
                Sector = sector.FirstOrDefault()
            };

            return vm;
        }
    }
}
