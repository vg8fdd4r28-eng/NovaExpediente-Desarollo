using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ExperienciasAmbientesDesarollo.Queries.GetListaExperienciasAmbientesDesarrollo
{
    public class GetExperienciaAmbienteDesarrolloListQueryHandler : IRequestHandler<GetExperienciaAmbienteDesarrolloListQuery, ExperienciaAmbienteDesarrolloListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetExperienciaAmbienteDesarrolloListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExperienciaAmbienteDesarrolloListVm> Handle(GetExperienciaAmbienteDesarrolloListQuery request, CancellationToken cancellationToken)
        {
            var experienciaAmbientes = await _context.ExperienciaAmbienteDesarrollos
                .Where(c => c.IDCOLABORADOR == request.Id)
                .OrderByDescending(e => e.MESESEXPERIENCIA)
                .Include(c => c.NivelConocimiento)
                .Include(c => c.AmbienteDesarrollo)
                .ProjectTo<ExperienciaAmbienteDesarrolloDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ExperienciaAmbienteDesarrolloListVm
            {
                ExperienciaAmbientesDesarrollo = experienciaAmbientes,
                Count = experienciaAmbientes.Count
            };

            return vm;
        }


    }
}
