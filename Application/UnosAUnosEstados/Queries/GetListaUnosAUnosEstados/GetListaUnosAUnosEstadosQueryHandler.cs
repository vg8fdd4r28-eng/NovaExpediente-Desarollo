using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.UnosAUnosEstados.Queries.GetListaUnosAUnosEstados
{
    public class GetListaUnosAUnosEstadosQueryHandler : IRequestHandler<GetListaUnosAUnosEstadosQuery, ListaUnosAUnosEstadosVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;
        public GetListaUnosAUnosEstadosQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListaUnosAUnosEstadosVm> Handle(GetListaUnosAUnosEstadosQuery request, CancellationToken cancellationToken)
        {
            var unosAUnosEstados = await _context.UnoAUnoEstados
              .Where(nc => nc.ACTIVO == true)
              .ProjectTo<UnosAUnosEstadosDto>(_mapper.ConfigurationProvider)
              .ToListAsync(cancellationToken);

            var vm = new ListaUnosAUnosEstadosVm
            {
                unosAUnosEstados = unosAUnosEstados,
                count = unosAUnosEstados.Count
            };

            return vm;
        }
    }
}
