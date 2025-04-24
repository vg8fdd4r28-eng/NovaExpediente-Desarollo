using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Certificaciones.Queries.GetListaCertificacionConsulta
{
    public class GetCertificacionConsultaListQueryHandler : IRequestHandler<GetCertificacionConsultaListQuery, CertificacionConsultaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCertificacionConsultaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CertificacionConsultaListVm> Handle(GetCertificacionConsultaListQuery request, CancellationToken cancellationToken)
        {
            var Certificacion = await _context.Certificacions
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<CertificacionConsultaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new CertificacionConsultaListVm
            {
                Certificaciones = Certificacion,
                Count = Certificacion.Count
            };

            return vm;
        }


    }
}
