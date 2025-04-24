using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.AplicacionMovil.Actividades.Queries.GetListaActividades
{
    public class GetListaActividadesQueryHandler : IRequestHandler<GetListaActividadesQuery, ListaActividadesVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetListaActividadesQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListaActividadesVm> Handle(GetListaActividadesQuery request, CancellationToken cancellationToken)
        {
            var actividades = await _context.Actividads
              .Where(nc => nc.ACTIVO == true)
              .Include(n => n.Idioma)
              .OrderBy(n => n.FECHACREACION)
              .ProjectTo<ActividadDto>(_mapper.ConfigurationProvider)
              .ToListAsync(cancellationToken);
            var vm = new ListaActividadesVm
            {
                Actividades = actividades,
                count = actividades.Count
            };
            return vm;
        }
    }
}
