using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.AplicacionMovil.Sorteos.Queries.GetListaSorteos
{
    public class GetListaSorteosQueryHandler : IRequestHandler<GetListaSorteosQuery, ListaSorteosVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetListaSorteosQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListaSorteosVm> Handle(GetListaSorteosQuery request, CancellationToken cancellationToken)
        {
            var sorteos = await _context.Sorteos
                //.Where(s => s.ACTIVO == true)
                .OrderBy(s => s.IDSORTEO)
                .ProjectTo<SorteoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            var vm = new ListaSorteosVm
            {
                Sorteos = sorteos,
                Count = sorteos.Count
            };
            return vm;
        }
    }
}
