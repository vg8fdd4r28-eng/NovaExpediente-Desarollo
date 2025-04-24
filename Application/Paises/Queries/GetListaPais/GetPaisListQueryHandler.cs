using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Paises.Queries.GetListaPais
{
    public class GetPaisListQueryHandler : IRequestHandler<GetPaisListQuery, PaisListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetPaisListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaisListVm> Handle(GetPaisListQuery request, CancellationToken cancellationToken)
        {
            var BusConsulta = await _context.Paises
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<PaisDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new PaisListVm
            {
                Paises = BusConsulta,
                Count = BusConsulta.Count
            };

            return vm;
        }


    }
}
