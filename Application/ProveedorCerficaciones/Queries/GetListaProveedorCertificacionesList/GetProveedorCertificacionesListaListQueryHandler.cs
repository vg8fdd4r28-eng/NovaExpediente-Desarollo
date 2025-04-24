using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.ProveedorCerficaciones.Queries.GetListaProveedorCertificacionesList
{
    public class GetProveedorCertificacionesListaListQueryHandler : IRequestHandler<GetProveedorCertificacionesListaListQuery, ProveedorCertificacionesListaListVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetProveedorCertificacionesListaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProveedorCertificacionesListaListVm> Handle(GetProveedorCertificacionesListaListQuery request, CancellationToken cancellationToken)
        {
            var proveedorCertificacion = await _context.ProveedorCertificacions
                .OrderBy(p => p.Nombre)
                .Include(p => p.ProveedorCertificaciones)
                .ProjectTo<ProveedorCertificacionesListaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ProveedorCertificacionesListaListVm
            {
                ProveedorCertificaciones = proveedorCertificacion,
                Count = proveedorCertificacion.Count
            };

            return vm;
        }

    }
}
