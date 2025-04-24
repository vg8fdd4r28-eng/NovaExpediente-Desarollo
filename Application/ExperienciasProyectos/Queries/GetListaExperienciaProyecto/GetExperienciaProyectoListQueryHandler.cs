using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ExperienciasProyectos.Queries.GetListaExperienciaProyecto
{
    public class GetExperienciaProyectoListQueryHandler : IRequestHandler<GetExperienciaProyectoListQuery, ExperienciaProyectoListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetExperienciaProyectoListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExperienciaProyectoListVm> Handle(GetExperienciaProyectoListQuery request, CancellationToken cancellationToken)
        {
            var ExperienciaProyectos = await _context.ExperienciaProyectos
                .Where(c => c.IDCOLABORADOR == request.Id)
                .OrderByDescending(e => e.FIN)
                .Include(c => c.Colaborador)
                .Include(c => c.HerramientaProyecto)
                .Include(c => c.TecnologiaProyecto)
                .Include(c => c.EmpresaCorporacion)
                .Include(c => c.Cliente)
                .Include(c => c.Contactos)
                //.Include(c => c.Cliente)
                .ProjectTo<ExperienciaProyectoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ExperienciaProyectoListVm
            {
                ExperienciaProyectos = ExperienciaProyectos,
                Count = ExperienciaProyectos.Count
            };

            return vm;
        }


    }
}
