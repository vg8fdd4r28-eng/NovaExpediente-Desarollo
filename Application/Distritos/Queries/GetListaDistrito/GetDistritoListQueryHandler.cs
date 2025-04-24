using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Distritos.Queries.GetListaDistrito
{
    public class GetDistritoListQueryHandler : IRequestHandler<GetDistritoListQuery, DistritoListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetDistritoListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DistritoListVm> Handle(GetDistritoListQuery request, CancellationToken cancellationToken)
        {
            var distritos = await _context.Distritos
                .OrderBy(c => c.NOMBRE)
                .Include(c => c.Canton)
                .Include(c => c.Canton.Provincia)
                .ProjectTo<DistritoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new DistritoListVm
            {
                Distritos = distritos,
                Count = distritos.Count
            };

            return vm;
        }


    }
}
