using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ExperienciasBaseDatos.Queries.GetListaExperienciasBaseDatos
{
    public class GetExperienciaBaseDatosListQueryHandler : IRequestHandler<GetExperienciaBaseDatosListQuery, ExperienciaBaseDatosListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetExperienciaBaseDatosListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExperienciaBaseDatosListVm> Handle(GetExperienciaBaseDatosListQuery request, CancellationToken cancellationToken)
        {
            var experienciaBD = await _context.ExperienciasBaseDatos
                .Where(c => c.IDCOLABORADOR == request.Id)
                .OrderByDescending(e => e.MESESEXPERIENCIA)
                .Include(c => c.NivelConocimiento)
                .Include(c => c.BaseDatos)
                .ProjectTo<ExperienciaBaseDatosDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ExperienciaBaseDatosListVm
            {
                ExperienciasBaseDatos = experienciaBD,
                Count = experienciaBD.Count
            };

            return vm;
        }


    }
}
