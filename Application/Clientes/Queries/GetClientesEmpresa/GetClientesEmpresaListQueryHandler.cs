using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Clientes.Queries.GetClientesEmpresa
{
    public class GetClientesEmpresaListQueryHandler : IRequestHandler<GetClientesEmpresaListQuery, ClientesEmpresaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetClientesEmpresaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClientesEmpresaListVm> Handle(GetClientesEmpresaListQuery request, CancellationToken cancellationToken)
        {
            var Cliente = await _context.Clientess
                .Where(c => c.IDEMPRESACORPORACION == request.Id)
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<ClientesEmpresaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ClientesEmpresaListVm
            {
                ClientesEmpresa = Cliente,
                Count = Cliente.Count
            };

            return vm;
        }
    }
}

