using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.NivelesIdiomas.Queries.GetListaNivelIdioma
{
    public class GetNivelIdiomaListQueryHandler : IRequestHandler<GetNivelIdiomaListQuery, NivelIdiomaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetNivelIdiomaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NivelIdiomaListVm> Handle(GetNivelIdiomaListQuery request, CancellationToken cancellationToken)
        {
            var NivelIdioma = await _context.NivelIdiomas
                .Include(p => p.Idioma)
                .OrderBy(p => p.Idioma.NOMBRE)
                .ProjectTo<NivelIdiomaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new NivelIdiomaListVm
            {
                NivelIdioma = NivelIdioma,
                Count = NivelIdioma.Count
            };

            return vm;
        }


    }
}
