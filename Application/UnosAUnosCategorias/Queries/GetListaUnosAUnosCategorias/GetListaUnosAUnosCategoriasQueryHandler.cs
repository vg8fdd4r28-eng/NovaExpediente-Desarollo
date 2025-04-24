using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.UnosAUnosCategorias.Queries.GetListaUnosAUnosCategorias
{
    public class GetListaUnosAUnosCategoriasQueryHandler : IRequestHandler<GetListaUnosAUnosCategoriasQuery, ListaUnosAUnosCategoriasVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;
        public GetListaUnosAUnosCategoriasQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListaUnosAUnosCategoriasVm> Handle(GetListaUnosAUnosCategoriasQuery request, CancellationToken cancellationToken)
        {
            var unosAUnosCategorias = await _context.UnoAUnoCategorias
              .Where(nc => nc.ACTIVO == true)
              .ProjectTo<UnosAUnosCategoriasDto>(_mapper.ConfigurationProvider)
              .ToListAsync(cancellationToken);

            var vm = new ListaUnosAUnosCategoriasVm
            {
                unosAUnosCategorias = unosAUnosCategorias,
                count = unosAUnosCategorias.Count
            };

            return vm;
        }
    }
}
