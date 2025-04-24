using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.CertificacionesColaborador.Queries.GetCertificacionColaboradorDetalle
{
    public class GetCertificacionColaboradorDetalleListQueryHandler : IRequestHandler<GetCertificacionColaboradorDetalleListQuery, CertificacionColaboradorDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCertificacionColaboradorDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CertificacionColaboradorDetalleVm> Handle(GetCertificacionColaboradorDetalleListQuery request, CancellationToken cancellationToken)
        {
            var certificacionColaborador = await _context.CertificacionColaboradors
                .Where(c => c.IDCERTIFICACIONCOLABORADOR == request.Id)
                .Select(c => c)
                .Include(c => c.Certificacion)
                .Include(c => c.CertificacionProveedor)
                .Include(c => c.CertificacionProveedor.ProveedorCertificacion)
                .ProjectTo<CertificacionColaboradorDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);



            var vm = new CertificacionColaboradorDetalleVm
            {
                CertificacionColaborador = certificacionColaborador.FirstOrDefault()
            };

            return vm;
        }


    }
}
