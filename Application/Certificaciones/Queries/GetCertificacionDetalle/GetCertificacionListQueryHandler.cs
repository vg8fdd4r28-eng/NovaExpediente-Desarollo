using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Certificaciones.Queries.GetCertificacionDetalle
{
    public class GetCertificacionDetalleListQueryHandler : IRequestHandler<GetCertificacionDetalleListQuery, CertificacionDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCertificacionDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CertificacionDetalleVm> Handle(GetCertificacionDetalleListQuery request, CancellationToken cancellationToken)
        {
            var Certificacion = await _context.Certificacions
                .Where(c => c.IDCERTIFICACION == request.Id)
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<CertificacionDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new CertificacionDetalleVm
            {
                Certificacion = Certificacion.FirstOrDefault()
         
            };

            return vm;
        }


    }
}
