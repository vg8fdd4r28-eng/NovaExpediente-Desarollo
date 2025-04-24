using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.CertificacionesColaborador.Queries.GetListaCertificacionColaborador
{
    public class GetCertificacionColaboradorListQueryHandler : IRequestHandler<GetCertificacionColaboradorListQuery, CertificacionColaboradorListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCertificacionColaboradorListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CertificacionColaboradorListVm> Handle(GetCertificacionColaboradorListQuery request, CancellationToken cancellationToken)
        {
            var certificacionesColaborador = await _context.CertificacionColaboradors
                .Where(c => c.IDCOLABORADOR == request.Id)
                .Select(c => c)
                .OrderByDescending(c => c.ANIO)
                .ThenBy(c=> c.Certificacion.NOMBRE)
                .Include(c => c.Certificacion)
                .ProjectTo<CertificacionColaboradorDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CertificacionColaboradorListVm
            {
                CertificacionesColaborador = certificacionesColaborador,
                Count = certificacionesColaborador.Count
            };

            return vm;
        }


    }
}
