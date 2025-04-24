using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.UnoAUnoEstadosColaboradores.Queries.GetListaUnoAUnoEstadosColaboradores
{
    public class GetListaUnosAUnosEstadosColaboradoresQueryHandler : IRequestHandler<GetListaUnosAUnosEstadosColaboradoresQuery, ListaUnosAUnosEstadosColaboradoresVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;
        public GetListaUnosAUnosEstadosColaboradoresQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListaUnosAUnosEstadosColaboradoresVm> Handle(GetListaUnosAUnosEstadosColaboradoresQuery request, CancellationToken cancellationToken)
        {
            var unosaunosEstadosColaboradores = await _context.UnoAUnoEstadosColaborador
              .Where(nc => nc.ACTIVO == true)
              .ProjectTo<UnosAUnosEstadosColaboradoresDto>(_mapper.ConfigurationProvider)
              .ToListAsync(cancellationToken);

            var vm = new ListaUnosAUnosEstadosColaboradoresVm
            {
                unosAUnosEstadosColaboradores = unosaunosEstadosColaboradores,
                count = unosaunosEstadosColaboradores.Count
            };

            return vm;
        }
    }
}
