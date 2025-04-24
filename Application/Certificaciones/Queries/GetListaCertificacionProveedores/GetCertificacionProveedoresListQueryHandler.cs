using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Certificaciones.Queries.GetListaCertificacionProveedores
{
    public class GetCertificacionProveedoresListQueryHandler : IRequestHandler<GetCertificacionProveedoresListQuery, CertificacionProveedoresListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCertificacionProveedoresListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CertificacionProveedoresListVm> Handle(GetCertificacionProveedoresListQuery request, CancellationToken cancellationToken)
        {
            var Certificacion = await _context.Certificacions
                .OrderBy(c => c.NOMBRE)
                .Include(c => c.CertificacionProveedores)
                .ProjectTo<CertificacionProveedoresDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new CertificacionProveedoresListVm
            {
                CertificacionesProveedores = Certificacion,
                Count = Certificacion.Count
            };

            return vm;
        }


    }
}
