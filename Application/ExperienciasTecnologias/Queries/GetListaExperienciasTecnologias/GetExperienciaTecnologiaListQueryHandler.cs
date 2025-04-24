using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ExperienciasTecnologias.Queries.GetListaExperienciasTecnologias
{
    public class GetExperienciaTecnologiaListQueryHandler : IRequestHandler<GetExperienciaTecnologiaListQuery, ExperienciaTecnologiaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetExperienciaTecnologiaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExperienciaTecnologiaListVm> Handle(GetExperienciaTecnologiaListQuery request, CancellationToken cancellationToken)
        {
            var experienciaTecnologias = await _context.ExperienciaTecnologias
                .Where(c => c.IDCOLABORADOR == request.Id)
                .OrderByDescending(e => e.MESESEXPERIENCIA)
                .Include(c => c.NivelConocimiento)
                .Include(c => c.Tecnologia)
                .ProjectTo<ExperienciaTecnologiaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ExperienciaTecnologiaListVm
            {
                ExperienciaTecnologias = experienciaTecnologias,
                Count = experienciaTecnologias.Count
            };

            return vm;
        }


    }
}
