using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Referencias.Queries.GetReferenciasXCliente
{
    public class GetReferenciaXClienteListQueryHandler : IRequestHandler<GetReferenciaXClienteListQuery, ReferenciaXClienteListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetReferenciaXClienteListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReferenciaXClienteListVm> Handle(GetReferenciaXClienteListQuery request, CancellationToken cancellationToken)
        {
            var Referencias = await _context.Referenciass
                .Where(e => e.IDCLIENTE == request.Id)
                .OrderBy(e => e.NOMBRE)
                .Include (e => e.Cliente)
                .ProjectTo<ReferenciaXClienteDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ReferenciaXClienteListVm
            {
                Referencias = Referencias,
                Count =Referencias.Count()
            };

            return vm;
        }


    }
}
