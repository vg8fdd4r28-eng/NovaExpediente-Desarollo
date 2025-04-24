using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;


namespace NovaExpediente.Application.AplicacionMovil.ConveniosCategorias.Queries.GetConveniosCategorias
{
    public class GetConveniosCategoriasQueryHandler : IRequestHandler<GetConveniosCategoriasQuery, ConveniosCategoriasVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetConveniosCategoriasQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ConveniosCategoriasVm> Handle(GetConveniosCategoriasQuery request, CancellationToken cancellationToken)
        {
            var conveniosCategorias = await _context.ConvenioCategorias
                .Where(nc => nc.ACTIVO == true)
                .OrderBy(nc => nc.IDCONVENIOCATEGORIA)
                .ProjectTo<ConveniosCategoriasDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            var vm = new ConveniosCategoriasVm
            {
                ConveniosCategorias = conveniosCategorias,
                Count = conveniosCategorias.Count
            };

            return vm;
        }
    }
}
