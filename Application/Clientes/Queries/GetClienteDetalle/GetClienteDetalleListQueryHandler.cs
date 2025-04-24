using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Clientes.Queries.GetClienteDetalle
{
    public class GetClienteDetalleListQueryHandler : IRequestHandler<GetClienteDetalleListQuery, ClienteDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetClienteDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClienteDetalleVm> Handle(GetClienteDetalleListQuery request, CancellationToken cancellationToken)
        {
            var Cliente = await _context.Clientess
                .Where(c => c.IDCLIENTE == request.Id)
                .OrderBy(p => p.NOMBRE)
                .ProjectTo<ClienteDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

       
            var vm = new ClienteDetalleVm
            {
                Cliente = Cliente.FirstOrDefault()
            };

            return vm;
        }


    }
}
