using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Proyectos.Queries.GetListaProyectos
{
    public class GetProyectoListQueryHandler : IRequestHandler<GetProyectoListQuery, ProyectoListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetProyectoListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProyectoListVm> Handle(GetProyectoListQuery request, CancellationToken cancellationToken)
        {
            var Proyectos = await _context.Proyectos
                .OrderBy(e => e.NOMBRE)
                .ProjectTo<ProyectoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ProyectoListVm
            {
                Proyectos = Proyectos,
                Count = Proyectos.Count
            };

            return vm;
        }


    }
}
