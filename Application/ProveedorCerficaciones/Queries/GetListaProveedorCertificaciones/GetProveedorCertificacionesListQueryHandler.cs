using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.ProveedorCerficaciones.Queries.GetListaProveedorCertificaciones
{
    public class GetProveedorCertificacionesListQueryHandler : IRequestHandler<GetProveedorCertificacionesListQuery, ProveedorCertificacionesListVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetProveedorCertificacionesListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProveedorCertificacionesListVm> Handle(GetProveedorCertificacionesListQuery request, CancellationToken cancellationToken)
        {
            var proveedorCertificacion = await _context.ProveedorCertificacions
               
                .OrderBy(p => p.Nombre)
                .ProjectTo<ProveedorCertificacionesDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ProveedorCertificacionesListVm
            {
                ProveedorCertificaciones = proveedorCertificacion,
                Count = proveedorCertificacion.Count
            };

            return vm;
        }

    }
}
