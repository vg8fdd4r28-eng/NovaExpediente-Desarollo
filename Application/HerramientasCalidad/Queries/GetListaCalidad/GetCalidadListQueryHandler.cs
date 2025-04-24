using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.HerramientasCalidad.Queries.GetListaCalidad
{
    public class GetCalidadListQueryHandler : IRequestHandler<GetCalidadListQuery, CalidadListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCalidadListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CalidadListVm> Handle(GetCalidadListQuery request, CancellationToken cancellationToken)
        {
            var Calidad = await _context.Calidads
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<CalidadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new CalidadListVm
            {
                Calidads = Calidad,
                Count = Calidad.Count
            };

            return vm;
        }


    }
}
