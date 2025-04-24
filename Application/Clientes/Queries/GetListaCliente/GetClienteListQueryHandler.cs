using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Clientes.Queries.GetListaCliente
{
    public class GetClienteListQueryHandler : IRequestHandler<GetClienteListQuery, ClienteListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetClienteListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClienteListVm> Handle(GetClienteListQuery request, CancellationToken cancellationToken)
        {
            var Cliente = await _context.Clientess
                .OrderBy(c => c.NOMBRE)
                .Include(c => c.EmpresaCorporacion)
                .ProjectTo<ClienteDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new ClienteListVm
            {
                Clientes = Cliente,
                Count = Cliente.Count
            };

            return vm;
        }


    }
}
