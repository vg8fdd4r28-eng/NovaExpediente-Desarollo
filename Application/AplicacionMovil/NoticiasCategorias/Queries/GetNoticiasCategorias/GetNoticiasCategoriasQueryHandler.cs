using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;


namespace NovaExpediente.Application.AplicacionMovil.NoticiasCategorias.Queries.GetNoticiasCategorias
{
    public class GetNoticiasCategoriasQueryHandler : IRequestHandler<GetNoticiasCategoriasQuery, NoticiasCategoriasVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetNoticiasCategoriasQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NoticiasCategoriasVm> Handle(GetNoticiasCategoriasQuery request, CancellationToken cancellationToken)
        {
            var noticiasCategorias = await _context.NoticiaCategoria
                .Where(nc => nc.ACTIVO == true)
                .OrderBy(nc => nc.IDNOTICIACATEGORIA)
                .ProjectTo<NoticiasCategoriasDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            var vm = new NoticiasCategoriasVm
            {
                NoticiasCategorias = noticiasCategorias,
                Count = noticiasCategorias.Count
            };

            return vm;
        }
    }
}
