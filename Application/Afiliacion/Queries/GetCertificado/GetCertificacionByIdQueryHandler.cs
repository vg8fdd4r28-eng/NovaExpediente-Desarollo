using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Afiliacion.Queries.GetCertificado
{
    public class GetCertificadoByIdQueryHandler : IRequestHandler<GetCertificadoByIdQuery, CertificadoAfiliacionVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;




        public GetCertificadoByIdQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CertificadoAfiliacionVm> Handle(GetCertificadoByIdQuery request, CancellationToken cancellationToken)
        {
            var afiliacion = await _context.AfiliacionColaborador
                .Where(c => c.IDAFILIACIONCOLABORADOR == request.Id)
                .ProjectTo<CertificadoAfiliacionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CertificadoAfiliacionVm
            {
                CertificadoAfiliacion = afiliacion.FirstOrDefault()
            };

            return vm;
        }


    }
}
