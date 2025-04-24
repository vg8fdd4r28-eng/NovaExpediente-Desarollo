using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.HerramientasCalidad.Queries.GetListaCalidadConsulta
{
    public class GetCalidadConsultaListQueryHandler : IRequestHandler<GetCalidadConsultaListQuery, CalidadConsultaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCalidadConsultaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CalidadConsultaListVm> Handle(GetCalidadConsultaListQuery request, CancellationToken cancellationToken)
        {
            var Calidad = await _context.Calidads
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<CalidadConsultaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new CalidadConsultaListVm
            {
                Calidads = Calidad,
                Count = Calidad.Count
            };

            return vm;
        }


    }
}
