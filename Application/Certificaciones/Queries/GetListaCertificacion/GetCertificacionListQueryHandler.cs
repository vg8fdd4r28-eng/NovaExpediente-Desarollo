using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Certificaciones.Queries.GetListaCertificacion
{
    public class GetCertificacionListQueryHandler : IRequestHandler<GetCertificacionListQuery, CertificacionListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCertificacionListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CertificacionListVm> Handle(GetCertificacionListQuery request, CancellationToken cancellationToken)
        {
            var Certificacion = await _context.Certificacions
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<CertificacionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new CertificacionListVm
            {
                Certificaciones = Certificacion,
                Count = Certificacion.Count
            };

            return vm;
        }


    }
}
