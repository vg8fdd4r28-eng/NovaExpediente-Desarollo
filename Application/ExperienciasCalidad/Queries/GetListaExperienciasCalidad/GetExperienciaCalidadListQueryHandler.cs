using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ExperienciasCalidad.Queries.GetListaExperienciasCalidad
{
    public class GetExperienciaCalidadListQueryHandler : IRequestHandler<GetExperienciaCalidadListQuery, ExperienciaCalidadListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetExperienciaCalidadListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExperienciaCalidadListVm> Handle(GetExperienciaCalidadListQuery request, CancellationToken cancellationToken)
        {
            var experienciaCalidads = await _context.ExperienciaCalidads
                .Where(c => c.IDCOLABORADOR == request.Id)
                .OrderByDescending(e => e.MESESEXPERIENCIA)
                .Include(c => c.NivelConocimiento)
                .Include(c => c.Calidad)
                .ProjectTo<ExperienciaCalidadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ExperienciaCalidadListVm
            {
                ExperienciasCalidad = experienciaCalidads,
                Count = experienciaCalidads.Count
            };

            return vm;
        }


    }
}
