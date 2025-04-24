using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Infraestructuras.Queries.GetInfraestructuraDetalle
{
    public class GetInfraestructuraDetalleListQueryHandler : IRequestHandler<GetInfraestructuraDetalleListQuery, InfraestructuraDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetInfraestructuraDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InfraestructuraDetalleVm> Handle(GetInfraestructuraDetalleListQuery request, CancellationToken cancellationToken)
        {
            var Infraestructura = await _context.Infraestructuras
                .Where(c => c.IDINFRAESTRUCTURA == request.Id)
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<InfraestructuraDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new InfraestructuraDetalleVm
            {
                Infraestructura = Infraestructura.FirstOrDefault()
            };

            return vm;
        }


    }
}
