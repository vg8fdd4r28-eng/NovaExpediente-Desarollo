using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Buses.Queries.GetBusDetalle
{
    public class GetBusListDetalleQueryHandler : IRequestHandler<GetBusDetalleListQuery, BusDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetBusListDetalleQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BusDetalleVm> Handle(GetBusDetalleListQuery request, CancellationToken cancellationToken)
        {
            var Bus = await _context.Buss
                .Where(c => c.IdBus == request.Id)
                .OrderBy(c => c.Nombre)
                .ProjectTo<BusDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new BusDetalleVm
            {
                Bus = Bus.FirstOrDefault()
            };

            return vm;
        }


    }
}
