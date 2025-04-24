using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.CursosColaborador.Queries.GetCursoColaboradorDocumento
{
    public class GetCursoColaboradorDocumentoByIdQueryHandler : IRequestHandler<GetCursoColaboradorDocumentoByIdQuery, CursoColaboradorDocumentoVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCursoColaboradorDocumentoByIdQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CursoColaboradorDocumentoVm> Handle(GetCursoColaboradorDocumentoByIdQuery request, CancellationToken cancellationToken)
        {
            var certificado = await _context.CursoColaboradors
                .Where(c => c.IDCURSOCOLABORADOR == request.Id)
                .ProjectTo<CursoColaboradorDocumentoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CursoColaboradorDocumentoVm
            {
                Certificado = certificado.FirstOrDefault()
            };

            return vm;
        }


    }
}
