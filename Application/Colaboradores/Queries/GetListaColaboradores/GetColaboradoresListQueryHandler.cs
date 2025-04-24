using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradores
{
    public class GetColaboradoresListQueryHandler : IRequestHandler<GetColaboradoresListQuery, ColaboradoresListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetColaboradoresListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ColaboradoresListVm> Handle(GetColaboradoresListQuery request, CancellationToken cancellationToken)
        {
            var colaboradores = await _context.Colaboradores
                .Include(p => p.Puesto)
                .Include(c => c.DeliveryManager)
                .Include(c => c.IdiomasColaborador)
                .Include(c => c.CertificacionesColaborador)
                .Include(c => c.Cliente)
                .ProjectTo<ColaboradoresDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new ColaboradoresListVm
            {
                Colaboradores = colaboradores,
                Count = colaboradores.Count
            };

            return vm;
        }


    }
}
