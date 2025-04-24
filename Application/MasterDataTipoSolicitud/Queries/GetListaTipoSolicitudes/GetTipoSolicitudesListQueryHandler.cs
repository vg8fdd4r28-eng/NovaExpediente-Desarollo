using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterDataTipoSolicitud.Queries.GetListaTipoSolicitudes
{
    public class GetTipoSolicitudesListQueryHandler : IRequestHandler<GetTipoSolicitudesListQuery, ListTipoSolicitudVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetTipoSolicitudesListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListTipoSolicitudVm> Handle(GetTipoSolicitudesListQuery request, CancellationToken cancellationToken)
        {
            var solicitudes = await _context.TipoSolicitud
                .OrderBy(p => p.NOMBRE)
                .ProjectTo<TipoSolicitudDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ListTipoSolicitudVm
            {
                TiposSolicitudes = solicitudes,
                count = solicitudes.Count
            };

            return vm;
        }
    }
}
