using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.CertificacionesColaborador.Queries.GetCertificacionDocumento
{
    public class GetCertificacionDocumentoByIdQueryHandler : IRequestHandler<GetCertificacionDocumentoByIdQuery, CertificacionDocumentoVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCertificacionDocumentoByIdQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CertificacionDocumentoVm> Handle(GetCertificacionDocumentoByIdQuery request, CancellationToken cancellationToken)
        {
            var certificado = await _context.CertificacionColaboradors
                .Where(c => c.IDCERTIFICACIONCOLABORADOR == request.Id)
                .ProjectTo<CertificacionDocumentoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CertificacionDocumentoVm
            {
                Certificado = certificado.FirstOrDefault()
            };

            return vm;
        }


    }
}
