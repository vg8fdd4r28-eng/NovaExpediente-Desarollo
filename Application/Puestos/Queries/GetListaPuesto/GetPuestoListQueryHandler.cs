using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Puestos.Queries.GetListaPuesto
{
    public class GetPuestoListQueryHandler : IRequestHandler<GetPuestoListQuery, PuestoListVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetPuestoListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PuestoListVm> Handle(GetPuestoListQuery request, CancellationToken cancellationToken)
        {
            var Puesto = await _context.Puestos

                .OrderBy(p => p.NOMBRE)
                .ProjectTo<PuestoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new PuestoListVm
            {
                Puesto = Puesto,
                Count = Puesto.Count
            };

            return vm;
        }
    }
}
