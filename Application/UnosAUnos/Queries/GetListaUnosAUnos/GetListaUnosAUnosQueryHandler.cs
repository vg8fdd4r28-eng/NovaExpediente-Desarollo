using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.UnosAUnos.Queries.GetListaUnosAUnos
{
    public class GetListaUnosAUnosQueryHandler : IRequestHandler<GetListaUnosAUnosQuery, ListaUnosAUnosVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;
        public GetListaUnosAUnosQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ListaUnosAUnosVm> Handle(GetListaUnosAUnosQuery request, CancellationToken cancellationToken)
        {
            var unosaunos = await _context.UnoAUnos
              .Where(nc => nc.ACTIVO == true && nc.IDUSUARIOCREA == request.IdDeliveryManager)
              .Include(n => n.Colaborador)
              .Include(n => n.UnoAUnoEstadoColaborador)
              .OrderBy(n => n.FECHACREACION)
              .ProjectTo<UnoAUnoDto>(_mapper.ConfigurationProvider)
              .ToListAsync(cancellationToken);

            var vm = new ListaUnosAUnosVm
            {
                unosAUnos = unosaunos,
                count = unosaunos.Count
            };

            return vm;
        }

    }
}
