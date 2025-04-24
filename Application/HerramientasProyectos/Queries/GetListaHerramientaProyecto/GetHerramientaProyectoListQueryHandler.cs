using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.HerramientasProyectos.Queries.GetListaHerramientaProyecto
{
    public class GetHerramientaProyectoListQueryHandler : IRequestHandler<GetHerramientaProyectoListQuery, HerramientaProyectoListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetHerramientaProyectoListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<HerramientaProyectoListVm> Handle(GetHerramientaProyectoListQuery request, CancellationToken cancellationToken)
        {
            var HerramientaProyectos = await _context.HerramientaProyectos
                .Where(c => c.IDEXPERIENCIAPROYECTO == request.Id)
                .OrderBy(e => e.NOMBRE)
                .ProjectTo<HerramientaProyectoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new HerramientaProyectoListVm
            {
                HerramientaProyectos = HerramientaProyectos,
                Count = HerramientaProyectos.Count
            };

            return vm;
        }


    }
}
