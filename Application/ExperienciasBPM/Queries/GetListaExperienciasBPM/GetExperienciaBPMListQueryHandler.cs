using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ExperienciasBPM.Queries.GetListaExperienciasBPM
{
    public class GetExperienciaBPMListQueryHandler : IRequestHandler<GetExperienciaBPMListQuery, ExperienciaBPMListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetExperienciaBPMListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExperienciaBPMListVm> Handle(GetExperienciaBPMListQuery request, CancellationToken cancellationToken)
        {
            var experienciaBPMs = await _context.ExperienciaBPMs
                .Where(c => c.IDCOLABORADOR == request.Id)
                .OrderByDescending(e => e.MESESEXPERIENCIA)
                .Include(c => c.NivelConocimiento)
                .Include(c => c.BPM)
                .ProjectTo<ExperienciaBPMDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ExperienciaBPMListVm
            {
                ExperienciasBPM = experienciaBPMs,
                Count = experienciaBPMs.Count
            };

            return vm;
        }


    }
}
