using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.TiposPermisoGoce.Queries.GetListaTipoPermisoGoce
{
    public class GetTipoPermisoGoceListQueryHandler : IRequestHandler<GetTipoPermisoGoceListQuery, TipoPermisoGoceListVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetTipoPermisoGoceListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TipoPermisoGoceListVm> Handle(GetTipoPermisoGoceListQuery request, CancellationToken cancellationToken)
        {
            var tipoPermiso = await _context.TipoPermisoGoces
                .Where(p => p.ESTADO == true)
                .OrderBy(p => p.NOMBRE)
                .ProjectTo<TipoPermisoGoceDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new TipoPermisoGoceListVm
            {
                TiposPermisoGoce = tipoPermiso,
                Count = tipoPermiso.Count
            };

            return vm;
        }
    }
}
