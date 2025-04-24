using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ExperienciasERP.Queries.GetListaExperienciasERP
{
    public class GetExperienciaERPListQueryHandler : IRequestHandler<GetExperienciaERPListQuery, ExperienciaERPListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetExperienciaERPListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExperienciaERPListVm> Handle(GetExperienciaERPListQuery request, CancellationToken cancellationToken)
        {
            var experienciaERPs = await _context.ExperienciaERPs
                .Where(c => c.IDCOLABORADOR == request.Id)
                .OrderByDescending(e => e.MESESEXPERIENCIA)
                .Include(c => c.NivelConocimiento)
                .Include(c => c.ErpCore)
                .Include(c => c.ErpCore.TipoCore)
                .ProjectTo<ExperienciaERPDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ExperienciaERPListVm
            {
                ExperienciasERPs = experienciaERPs,
                Count = experienciaERPs.Count
            };

            return vm;
        }


    }
}
