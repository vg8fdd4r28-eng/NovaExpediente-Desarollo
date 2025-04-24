using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Colaboradores.Queries.GetDetalleColaboradorResumen
{
    public class GetColaboradorResumenListKeyQueryHandler : IRequestHandler<GetColaboradorResumenListKeyQuery, ColaboradorResumenVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;




        public GetColaboradorResumenListKeyQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ColaboradorResumenVm> Handle(GetColaboradorResumenListKeyQuery request, CancellationToken cancellationToken)
        {
            var colaborador = await _context.Colaboradores
                .Where(c => c.IDCOLABORADOR == request.Id)
                .Include(c => c.DeliveryManager)
                .Include(c => c.EmpresaCorporacion)
                .Include(c => c.Puesto)
                .Include(c => c.Cliente)
                .Include(c => c.PaisResidencia)
                .ProjectTo<ColaboradorResumenDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ColaboradorResumenVm
            {
                Colaborador = colaborador.FirstOrDefault()
            };

            return vm;
        }


    }
}
