using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.HerramientasCalidad.Queries.GetCalidadDetalle
{
    public class GetCalidadDetalleListQueryHandler : IRequestHandler<GetCalidadDetalleListQuery, CalidadDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCalidadDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CalidadDetalleVm> Handle(GetCalidadDetalleListQuery request, CancellationToken cancellationToken)
        {
            var Calidad = await _context.Calidads
                .Where(c => c.IDCALIDAD == request.Id)
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<CalidadDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CalidadDetalleVm
            {
                Calidad = Calidad.FirstOrDefault()
            };

            return vm;
        }


    }
}
