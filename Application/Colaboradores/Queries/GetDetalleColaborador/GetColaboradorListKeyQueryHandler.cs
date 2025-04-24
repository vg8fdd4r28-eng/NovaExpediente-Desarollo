using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Colaboradores.Queries.GetDetalleColaborador
{
    public class GetColaboradorListKeyQueryHandler : IRequestHandler<GetColaboradorListKeyQuery, ColaboradorVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;




        public GetColaboradorListKeyQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ColaboradorVm> Handle(GetColaboradorListKeyQuery request, CancellationToken cancellationToken)
        {
            var colaborador = await _context.Colaboradores
                .Include(c => c.Puesto)
                .Include(c => c.PaisOrigen)
                .Include(c => c.PaisResidencia)
                .Include(c => c.PaisFeriado)
                .Include(c => c.Canton)
                .Include(c => c.Cliente)
                .Include(c => c.AreaOrganizativa)
                .Include(c => c.DeliveryManager)
                .Include(c => c.EmpresaCorporacion)
                .Include(c => c.AfiliacionesColaborador)
                .Where(c => c.IDCOLABORADOR == request.Id)
                .ProjectTo<ColaboradorDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ColaboradorVm
            {
                Colaborador = colaborador.FirstOrDefault()
            };

            return vm;
        }


    }
}
