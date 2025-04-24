using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.NivelesIdiomas.Queries.GetListaNivelIdiomaXdioma
{
    public class GetNivelIdiomaXdiomaListQueryHandler : IRequestHandler<GetNivelIdiomaXdiomaListQuery, NivelIdiomaXdiomaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetNivelIdiomaXdiomaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NivelIdiomaXdiomaListVm> Handle(GetNivelIdiomaXdiomaListQuery request, CancellationToken cancellationToken)
        {
            var NivelIdioma = await _context.NivelIdiomas
                .Where(c => c.IDIDIOMA == request.Id)
                .Include(p => p.Idioma)
                .OrderBy(p => p.Idioma.NOMBRE)
                .ProjectTo<NivelIdiomaXdiomaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new NivelIdiomaXdiomaListVm
            {
                NivelesIdioma = NivelIdioma,
                Count = NivelIdioma.Count
            };

            return vm;
        }


    }
}
