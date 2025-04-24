using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Referencias.Queries.GetListaReferencias
{
    public class GetReferenciaListQueryHandler : IRequestHandler<GetReferenciaListQuery, ReferenciaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetReferenciaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReferenciaListVm> Handle(GetReferenciaListQuery request, CancellationToken cancellationToken)
        {
            var Referencias = await _context.Referenciass
                .OrderBy(e => e.NOMBRE)
                .ProjectTo<ReferenciaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ReferenciaListVm
            {
                Referencias = Referencias,
                Count = Referencias.Count
            };

            return vm;
        }


    }
}
