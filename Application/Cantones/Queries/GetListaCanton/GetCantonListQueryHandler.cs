using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Cantones.Queries.GetListaCanton
{
    public class GetCantonListQueryHandler : IRequestHandler<GetCantonListQuery, CantonListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCantonListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CantonListVm> Handle(GetCantonListQuery request, CancellationToken cancellationToken)
        {
            var cantones = await _context.Cantones
                .OrderBy(c => c.NOMBRE)
                .Include(c => c.Provincia)
                .ProjectTo<CantonDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CantonListVm
            {
                Cantones = cantones,
                Count = cantones.Count
            };

            return vm;
        }


    }
}
