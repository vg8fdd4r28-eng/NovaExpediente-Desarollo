using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Clientes.Queries.GetListaClienteReferencias
{
    public class GetClienteReferenciasListQueryHandler : IRequestHandler<GetClienteReferenciasListQuery, ClienteReferenciasListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetClienteReferenciasListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClienteReferenciasListVm> Handle(GetClienteReferenciasListQuery request, CancellationToken cancellationToken)
        {
            var Cliente = await _context.Clientess
                .OrderBy(c => c.NOMBRE)
                .Include(c => c.Referencias)
                .Include(c => c.Pais)
                .Include(c => c.Sector)
                .Include(c => c.EmpresaCorporacion)
                .ProjectTo<ClienteReferenciasDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new ClienteReferenciasListVm
            {
                ClientesReferencias = Cliente,
                Count = Cliente.Count
            };

            return vm;
        }


    }
}
