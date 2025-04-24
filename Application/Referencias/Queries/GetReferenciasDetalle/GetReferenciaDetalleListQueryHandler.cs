using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Referencias.Queries.GetReferenciasDetalle
{
    public class GetReferenciaDetalleListQueryHandler : IRequestHandler<GetReferenciaDetalleListQuery, ReferenciaDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetReferenciaDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReferenciaDetalleVm> Handle(GetReferenciaDetalleListQuery request, CancellationToken cancellationToken)
        {
            var Referencias = await _context.Referenciass
                .Where(e => e.IDREFERENCIA == request.Id)
                .OrderBy(e => e.NOMBRE)
                .ProjectTo<ReferenciaDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ReferenciaDetalleVm
            {
                Referencia = Referencias.FirstOrDefault()
            };

            return vm;
        }


    }
}
