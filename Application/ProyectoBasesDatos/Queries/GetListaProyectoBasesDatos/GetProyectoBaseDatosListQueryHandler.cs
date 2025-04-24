using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ProyectoBasesDatos.Queries.GetListaProyectoBaseDatos
{
    public class GetProyectoBaseDatosListQueryHandler : IRequestHandler<GetProyectoBaseDatosListQuery, ProyectoBaseDatosListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetProyectoBaseDatosListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProyectoBaseDatosListVm> Handle(GetProyectoBaseDatosListQuery request, CancellationToken cancellationToken)
        {
            var ProyectoBaseDatoss = await _context.ProyectoBaseDatoss
                .Where(c => c.IDPROYECTO == request.Id)
                .OrderBy(e => e.IDPROYECTO)
                .Include(c => c.BaseDatos)
                .ProjectTo<ProyectoBaseDatosDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ProyectoBaseDatosListVm
            {
                ProyectoBaseDatos = ProyectoBaseDatoss,
                Count = ProyectoBaseDatoss.Count
            };

            return vm;
        }


    }
}
