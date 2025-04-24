using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ExperienciasProyectos.Queries.GetExperienciaProyectoDetalle
{
    public class GetExperienciaProyectoDetalleListQueryHandler : IRequestHandler<GetExperienciaProyectoDetalleListQuery, ExperienciaProyectoDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetExperienciaProyectoDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExperienciaProyectoDetalleVm> Handle(GetExperienciaProyectoDetalleListQuery request, CancellationToken cancellationToken)
        {
            var ExperienciaProyectos = await _context.ExperienciaProyectos
                .Where(c => c.IDEXPERIENCIAPROYECTO == request.Id)
                .OrderBy(e => e.NOMBREPROYECTO)
               // .Include(c => c.Colaborador)
               // .Include(c => c.HerramientaProyecto)
              //  .Include(c => c.TecnologiaProyecto)
                .ProjectTo<ExperienciaProyectoDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ExperienciaProyectoDetalleVm
            {
                ExperienciaProyecto = ExperienciaProyectos.FirstOrDefault()
            };

            return vm;
        }


    }
}
