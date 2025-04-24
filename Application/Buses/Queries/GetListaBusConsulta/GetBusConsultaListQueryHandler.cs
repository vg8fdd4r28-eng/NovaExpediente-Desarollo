using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Buses.Queries.GetListaBusConsulta
{
    public class GetBusConsultaListQueryHandler : IRequestHandler<GetBusConsultaListQuery, BusConsultaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetBusConsultaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BusConsultaListVm> Handle(GetBusConsultaListQuery request, CancellationToken cancellationToken)
        {
            var BusConsulta = await _context.Buss
                .OrderBy(c => c.Nombre)
                .ProjectTo<BusConsultaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new BusConsultaListVm
            {
                Buses = BusConsulta,
                Count = BusConsulta.Count
            };

            return vm;
        }


    }
}
