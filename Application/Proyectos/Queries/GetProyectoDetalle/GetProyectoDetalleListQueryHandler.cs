using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Proyectos.Queries.GetProyectoDetalle
{
    public class GetProyectoDetalleListQueryHandler : IRequestHandler<GetProyectoDetalleListQuery, ProyectoDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetProyectoDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProyectoDetalleVm> Handle(GetProyectoDetalleListQuery request, CancellationToken cancellationToken)
        {
            var Proyecto = await _context.Proyectos
                .Where(c => c.IDPROYECTO == request.Id)
                .OrderBy(e => e.NOMBRE)
                .Include(c => c.Cliente)
                .ProjectTo<ProyectoDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ProyectoDetalleVm
            {
                Proyecto = Proyecto.FirstOrDefault()
            };

            return vm;
        }


    }
}
