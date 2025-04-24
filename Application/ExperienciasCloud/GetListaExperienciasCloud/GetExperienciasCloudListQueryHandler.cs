using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ExperienciasCloud.Queries.GetListaExperienciasCloud
{
    public class GetExperienciasCloudListQueryHandler : IRequestHandler<GetExperienciasCloudListQuery, ExperienciasCloudListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetExperienciasCloudListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExperienciasCloudListVm> Handle(GetExperienciasCloudListQuery request, CancellationToken cancellationToken)
        {
            var experienciaCloud = await _context.ExperienciaClouds
                .Where(c => c.IDCOLABORADOR == request.Id)
                .OrderByDescending(e => e.MESESEXPERIENCIA)
                .Include(c => c.NivelConocimiento)
                .Include(c => c.Cloud)
                .ProjectTo<ExperienciaCloudDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ExperienciasCloudListVm
            {
                ExperienciaCloud = experienciaCloud,
                Count = experienciaCloud.Count
            };

            return vm;
        }


    }
}
