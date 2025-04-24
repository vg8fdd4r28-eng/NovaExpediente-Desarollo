using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Infraestructuras.Queries.GetListaInfraestructura
{
    public class GetInfraestructuraListQueryHandler : IRequestHandler<GetInfraestructuraListQuery, InfraestructuraListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetInfraestructuraListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InfraestructuraListVm> Handle(GetInfraestructuraListQuery request, CancellationToken cancellationToken)
        {
            var Infraestructura = await _context.Infraestructuras
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<InfraestructuraDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new InfraestructuraListVm
            {
                Infraestructuras = Infraestructura,
                Count = Infraestructura.Count
            };

            return vm;
        }


    }
}
