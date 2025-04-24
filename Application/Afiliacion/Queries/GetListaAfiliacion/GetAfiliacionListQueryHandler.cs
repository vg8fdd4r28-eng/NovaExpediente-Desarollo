using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Afiliacion.Queries.GetAfiliacionDetalle;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Afiliacion.Queries.GetListaAfiliacion
{
    public class GetAfiliacionListQueryHandler : IRequestHandler<GetAfiliacionListQuery, AfiliacionListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetAfiliacionListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AfiliacionListVm> Handle(GetAfiliacionListQuery request, CancellationToken cancellationToken)
        {
            var afiliaciones = await _context.AfiliacionColaborador
                .Where(c => c.IDCOLABORADOR == request.Id)
                .OrderByDescending(x => x.FECHAINICIO)
                .ThenBy(x => x.NOMBRE)
                .ProjectTo<AfiliacionColaboradorBaseDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new AfiliacionListVm
            {
                Afiliaciones = afiliaciones,
                Count = afiliaciones.Count
            };

            return vm;
        }


    }
}
