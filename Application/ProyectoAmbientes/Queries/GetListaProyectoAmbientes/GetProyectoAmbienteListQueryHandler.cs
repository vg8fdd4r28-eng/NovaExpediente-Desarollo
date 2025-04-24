using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ProyectoAmbientes.Queries.GetListaProyectoAmbientes
{
    public class GetProyectoAmbienteListQueryHandler : IRequestHandler<GetProyectoAmbienteListQuery, ProyectoAmbienteListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetProyectoAmbienteListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProyectoAmbienteListVm> Handle(GetProyectoAmbienteListQuery request, CancellationToken cancellationToken)
        {
            var ProyectoAmbientes = await _context.ProyectoAmbientes
                .Where(c => c.IDPROYECTO == request.Id)
                .OrderBy(e => e.IDPROYECTO)
                .Include(c => c.AmbienteDesarrollo)
                .ProjectTo<ProyectoAmbienteDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ProyectoAmbienteListVm
            {
                ProyectoAmbientes = ProyectoAmbientes,
                Count = ProyectoAmbientes.Count
            };

            return vm;
        }


    }
}
