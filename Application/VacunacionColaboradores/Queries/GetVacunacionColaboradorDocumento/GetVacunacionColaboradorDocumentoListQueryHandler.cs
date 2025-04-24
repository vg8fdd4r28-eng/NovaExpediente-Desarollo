using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.VacunacionColaboradores.Queries.GetVacunacionColaboradorDocumento
{
    public class GetVacunacionColaboradorDocumentoListQueryHandler : IRequestHandler<GetVacunacionColaboradorDocumentoListQuery, VacunacionColaboradorDocumentoVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetVacunacionColaboradorDocumentoListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VacunacionColaboradorDocumentoVm> Handle(GetVacunacionColaboradorDocumentoListQuery request, CancellationToken cancellationToken)
        {
            var vacunacionColaborador = await _context.VacunacionColaboradores
                .Where(c => c.IdColaborador == request.IdColaborador)
                .ProjectTo<VacunacionColaboradorDocumentoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new VacunacionColaboradorDocumentoVm
            {
                VacunacionColaborador = vacunacionColaborador.FirstOrDefault()
            };

            return vm;
        }


    }
}
