using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.CertificacionesProveedor.Queries.GetListaProveedorXCertificacion
{
    public class GetCertificacionProveedorXCertificacionListQueryHandler : IRequestHandler<GetCertificacionProveedorXCertificacionListQuery, CertificacionProveedorXCertificacionListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCertificacionProveedorXCertificacionListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CertificacionProveedorXCertificacionListVm> Handle(GetCertificacionProveedorXCertificacionListQuery request, CancellationToken cancellationToken)
        {
            var proveedoresXCertificacion = await _context.CertificacionProveedors
                .Where(c => c.IDCERTIFICACION == request.Id)
                .Select(c => c)
                .OrderBy(c => c.ProveedorCertificacion.Nombre)
                .Include(c => c.ProveedorCertificacion)
                .ProjectTo<CertificacionProveedorXCertificacionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CertificacionProveedorXCertificacionListVm
            {
                CertificacionesProveedor = proveedoresXCertificacion,
                Count = proveedoresXCertificacion.Count
            };

            return vm;
        }


    }
}
