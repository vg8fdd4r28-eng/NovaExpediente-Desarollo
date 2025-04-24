using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Colaboradores.Queries.GetCertificadoCPIC
{
    public class GetCertificadoCPICByIdQueryHandler : IRequestHandler<GetCertificadoCPICByIdQuery, CertificadoCPICVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCertificadoCPICByIdQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CertificadoCPICVm> Handle(GetCertificadoCPICByIdQuery request, CancellationToken cancellationToken)
        {
            var certificado = await _context.Colaboradores
                .Where(c => c.IDCOLABORADOR == request.Id)
                .ProjectTo<CertificadoCPICDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CertificadoCPICVm
            {
                CertificadoCPIC = certificado.FirstOrDefault()
            };

            return vm;
        }


    }
}
