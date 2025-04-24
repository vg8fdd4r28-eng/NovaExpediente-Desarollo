using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.AplicacionMovil.Noticias.Queries.GetListaNoticias
{
    public class GetListaNoticiasQueryHandler : IRequestHandler<GetListaNoticiasQuery, ListaNoticiasVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetListaNoticiasQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListaNoticiasVm> Handle(GetListaNoticiasQuery request, CancellationToken cancellationToken)
        {
            var noticias = await _context.Noticias
              .Where(nc => nc.ACTIVO == true)
              .Include(n => n.Idioma) 
              .OrderBy(n => n.FECHACREACION)
              .ProjectTo<NoticiaDto>(_mapper.ConfigurationProvider)
              .ToListAsync(cancellationToken);
            var vm = new ListaNoticiasVm
            {
                Noticias = noticias,
                count = noticias.Count
            };
            return vm;
        }
    }
}
