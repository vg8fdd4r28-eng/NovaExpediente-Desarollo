using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.NivelesIdiomas.Queries.GetListaNivelIdiomaConsulta
{
    public class GetNivelIdiomaConsultaListQueryHandler : IRequestHandler<GetNivelIdiomaConsultaListQuery, NivelIdiomaConsultaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetNivelIdiomaConsultaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NivelIdiomaConsultaListVm> Handle(GetNivelIdiomaConsultaListQuery request, CancellationToken cancellationToken)
        {
            var NivelIdioma = await _context.NivelIdiomas
                .Include(p => p.Idioma)
                .OrderBy(p => p.Idioma.NOMBRE)
                .ProjectTo<NivelIdiomaConsultaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new NivelIdiomaConsultaListVm
            {
                NivelIdioma = NivelIdioma,
                Count = NivelIdioma.Count
            };

            return vm;
        }


    }
}
