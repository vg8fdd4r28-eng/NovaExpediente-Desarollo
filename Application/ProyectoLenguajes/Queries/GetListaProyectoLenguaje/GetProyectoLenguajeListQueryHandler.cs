using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ProyectoLenguajes.Queries.GetListaProyectoLenguaje
{
    public class GetProyectoLenguajeListQueryHandler : IRequestHandler<GetProyectoLenguajeListQuery, ProyectoLenguajeListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetProyectoLenguajeListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProyectoLenguajeListVm> Handle(GetProyectoLenguajeListQuery request, CancellationToken cancellationToken)
        {
            var ProyectoLenguajes = await _context.ProyectoLenguajes
                .Where(c => c.IDPROYECTO == request.Id)
                .OrderBy(e => e.IDPROYECTO)
                .Include(c => c.LenguajeProgramacion)
                .ProjectTo<ProyectoLenguajeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ProyectoLenguajeListVm
            {
                ProyectoLenguaje = ProyectoLenguajes,
                Count = ProyectoLenguajes.Count
            };

            return vm;
        }


    }
}
