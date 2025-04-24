using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ProveedorCerficaciones.Queries.GetProveedorCertificacionesDetalle
{
    public class GetProveedorCertificacionesDetalleListQueryHandler : IRequestHandler<GetProveedorCertificacionesDetalleListQuery, ProveedorCertificacionesDetalleVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetProveedorCertificacionesDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProveedorCertificacionesDetalleVm> Handle(GetProveedorCertificacionesDetalleListQuery request, CancellationToken cancellationToken)
        {
            var proveedorCertificacion = await _context.ProveedorCertificacions
                .Where(p => p.IDPROVEEDORCERTIFICACION == request.Id)
                .OrderBy(p => p.Nombre)
                .ProjectTo<ProveedorCertificacionesDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ProveedorCertificacionesDetalleVm
            {
                ProveedorCertificacion = proveedorCertificacion.FirstOrDefault()
            };

            return vm;
        }

    }
}
