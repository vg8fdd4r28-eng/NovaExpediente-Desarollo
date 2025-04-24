using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Idiomas.Queries.GetIdiomasDetalle
{
    public class GetIdiomaDetalleListQueryHandler : IRequestHandler<GetIdiomaDetalleListQuery, IdiomaDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetIdiomaDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IdiomaDetalleVm> Handle(GetIdiomaDetalleListQuery request, CancellationToken cancellationToken)
        {
            var idioma = await _context.Idiomas
                .Where(p => p.IDIDIOMA == request.Id)
                .OrderBy(p => p.NOMBRE)
                .ProjectTo<IdiomaDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new IdiomaDetalleVm
            {
                Idioma = idioma.FirstOrDefault()
            };

            return vm;
        }
    }
}
