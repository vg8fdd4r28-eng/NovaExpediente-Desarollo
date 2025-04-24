using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ExperienciasBus.Queries.GetListaExperienciasBus
{
    public class GetExperienciaBusListQueryHandler : IRequestHandler<GetExperienciaBusListQuery, ExperienciaBusListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetExperienciaBusListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExperienciaBusListVm> Handle(GetExperienciaBusListQuery request, CancellationToken cancellationToken)
        {
            var experienciaBuss = await _context.ExperienciaBuss
                .Where(c => c.IDCOLABORADOR == request.Id)
                .OrderByDescending(e => e.MESESEXPERIENCIA)
                .Include(c => c.NivelConocimiento)
                .Include(c => c.Bus)
                .ProjectTo<ExperienciaBusDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ExperienciaBusListVm
            {
                ExperienciasBus = experienciaBuss,
                Count = experienciaBuss.Count
            };

            return vm;
        }


    }
}
