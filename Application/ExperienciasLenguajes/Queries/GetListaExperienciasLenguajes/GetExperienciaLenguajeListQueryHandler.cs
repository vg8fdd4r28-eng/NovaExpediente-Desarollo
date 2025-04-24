using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ExperienciasLenguajes.Queries.GetListaExperienciasLenguajes
{
    public class GetExperienciaLenguajeListQueryHandler : IRequestHandler<GetExperienciaLenguajeListQuery, ExperienciaLenguajeListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetExperienciaLenguajeListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExperienciaLenguajeListVm> Handle(GetExperienciaLenguajeListQuery request, CancellationToken cancellationToken)
        {
            var experienciaenguajes = await _context.ExperienciaLenguajes
                .Where(c => c.IDCOLABORADOR == request.Id)
                .OrderByDescending(e => e.MESESEXPERIENCIA)
                .Include(c => c.NivelConocimiento)
                .Include(c => c.LenguajeProgramacion)
                .ProjectTo<ExperienciaLenguajeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ExperienciaLenguajeListVm
            {
                ExperienciaLenguajes = experienciaenguajes,
                Count = experienciaenguajes.Count
            };

            return vm;
        }


    }
}
