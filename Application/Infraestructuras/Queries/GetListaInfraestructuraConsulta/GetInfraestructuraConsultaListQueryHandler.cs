using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Infraestructuras.Queries.GetListaInfraestructuraConsulta
{
    public class GetInfraestructuraConsultaListQueryHandler : IRequestHandler<GetInfraestructuraConsultaListQuery, InfraestructuraConsultaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetInfraestructuraConsultaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InfraestructuraConsultaListVm> Handle(GetInfraestructuraConsultaListQuery request, CancellationToken cancellationToken)
        {
            var Infraestructura = await _context.Infraestructuras
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<InfraestructuraConsultaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new InfraestructuraConsultaListVm
            {
                Infraestructuras = Infraestructura,
                Count = Infraestructura.Count
            };

            return vm;
        }


    }
}
