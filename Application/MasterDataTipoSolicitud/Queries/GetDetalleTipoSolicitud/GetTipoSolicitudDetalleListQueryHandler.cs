using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterDataTipoSolicitud.Queries.GetDetalleTipoSolicitud
{
    public class GetTipoSolicitudDetalleListQueryHandler : IRequestHandler<GetTipoSolicitudDetalleListQuery, TipoSolicitudDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetTipoSolicitudDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TipoSolicitudDetalleVm> Handle(GetTipoSolicitudDetalleListQuery request, CancellationToken cancellationToken)
        {
            var solicitudes = await _context.TipoSolicitud
                .Where(p => p.IDTIPOSOLICITUD == request.Id)
                .OrderBy(p => p.NOMBRE)
                .ProjectTo<TipoSolicitudDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new TipoSolicitudDetalleVm
            {
                TipoSolicitud = solicitudes.FirstOrDefault()
            };

            return vm;
        }
    }
}
