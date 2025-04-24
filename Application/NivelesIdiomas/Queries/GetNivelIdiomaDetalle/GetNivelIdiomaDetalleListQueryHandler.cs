using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.NivelesIdiomas.Queries.GetNivelIdiomaDetalle
{
    public class GetNivelIdiomaDetalleListQueryHandler : IRequestHandler<GetNivelIdiomaDetalleListQuery, NivelIdiomaDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetNivelIdiomaDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NivelIdiomaDetalleVm> Handle(GetNivelIdiomaDetalleListQuery request, CancellationToken cancellationToken)
        {
            var NivelIdioma = await _context.NivelIdiomas
                .Where(c => c.IDNIVELIDIOMA == request.Id)
                .Include(p => p.Idioma)
                .OrderBy(p => p.Idioma.NOMBRE)
                .ProjectTo<NivelIdiomaDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

       
            var vm = new NivelIdiomaDetalleVm
            {
                NivelIdioma = NivelIdioma.FirstOrDefault()
            };

            return vm;
        }


    }
}
