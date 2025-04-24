using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.AplicacionMovil.Convenios.Queries.GetListaConvenios
{
    public class GetListaConveniosQueryHandler : IRequestHandler<GetListaConveniosQuery, ListaConveniosVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetListaConveniosQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListaConveniosVm> Handle(GetListaConveniosQuery request, CancellationToken cancellationToken)
        {
            var convenios = await _context.Convenios
              .Where(nc => nc.ACTIVO == true)
              .Include(n => n.Idioma)
              .OrderBy(n => n.FECHACREACION)
              .ProjectTo<ConvenioDto>(_mapper.ConfigurationProvider)
              .ToListAsync(cancellationToken);
            var vm = new ListaConveniosVm
            {
                Convenios = convenios,
                count = convenios.Count
            };
            return vm;
        }
    }
}
