using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Buses.Queries.GetListaBus
{
    public class GetBusListQueryHandler : IRequestHandler<GetBusListQuery, BusListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetBusListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BusListVm> Handle(GetBusListQuery request, CancellationToken cancellationToken)
        {
            var Bus = await _context.Buss
                .OrderBy(c => c.Nombre)
                .ProjectTo<BusDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new BusListVm
            {
                Buses = Bus,
                Count = Bus.Count
            };

            return vm;
        }


    }
}
