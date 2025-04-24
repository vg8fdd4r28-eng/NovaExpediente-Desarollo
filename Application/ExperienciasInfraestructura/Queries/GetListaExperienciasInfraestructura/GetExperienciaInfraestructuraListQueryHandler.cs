using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ExperienciasInfraestructura.Queries.GetListaExperienciasInfraestructura
{
    public class GetExperienciaInfraestructuraListQueryHandler : IRequestHandler<GetExperienciaInfraestructuraListQuery, ExperienciaInfraestructuraListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetExperienciaInfraestructuraListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExperienciaInfraestructuraListVm> Handle(GetExperienciaInfraestructuraListQuery request, CancellationToken cancellationToken)
        {
            var experienciaInfraestructuras = await _context.ExperienciaInfraestructuras
                .Where(c => c.IDCOLABORADOR == request.Id)
                .OrderByDescending(e => e.MESESEXPERIENCIA)
                .Include(c => c.NivelConocimiento)
                .Include(c => c.Infraestructura)
                .ProjectTo<ExperienciaInfraestructuraDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ExperienciaInfraestructuraListVm
            {
                ExperienciasInfraestructuras = experienciaInfraestructuras,
                Count = experienciaInfraestructuras.Count
            };

            return vm;
        }


    }
}
