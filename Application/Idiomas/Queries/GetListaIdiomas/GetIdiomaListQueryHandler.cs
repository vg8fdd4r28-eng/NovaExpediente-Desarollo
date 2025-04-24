using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Idiomas.Queries.GetListaIdiomas
{
    public class GetIdiomaListQueryHandler : IRequestHandler<GetIdiomaListQuery, IdiomaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetIdiomaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IdiomaListVm> Handle(GetIdiomaListQuery request, CancellationToken cancellationToken)
        {
            var idioma = await _context.Idiomas

                .OrderBy(p => p.NOMBRE)
                .ProjectTo<IdiomaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new IdiomaListVm
            {
                Idiomas = idioma,
                count = idioma.Count
            };

            return vm;
        }
    }
}
