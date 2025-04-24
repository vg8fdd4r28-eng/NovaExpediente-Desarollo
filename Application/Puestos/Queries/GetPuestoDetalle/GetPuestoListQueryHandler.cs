using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Puestos.Queries.GetPuestoDetalle
{
    public class GetPuestoDetalleListQueryHandler : IRequestHandler<GetPuestoDetalleListQuery, PuestoDetalleVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetPuestoDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PuestoDetalleVm> Handle(GetPuestoDetalleListQuery request, CancellationToken cancellationToken)
        {
            var Puesto = await _context.Puestos
                .Where(p => p.IDPUESTO == request.Id)
                .OrderBy(p => p.NOMBRE)
                .ProjectTo<PuestoDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new PuestoDetalleVm
            {
                Puesto = Puesto.FirstOrDefault()
            };

            return vm;
        }
    }
}
