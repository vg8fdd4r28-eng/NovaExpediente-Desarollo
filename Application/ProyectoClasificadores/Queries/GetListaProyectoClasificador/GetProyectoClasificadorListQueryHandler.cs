using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ProyectoClasificadores.Queries.GetListaProyectoClasificador
{
    public class GetProyectoClasificadorListQueryHandler : IRequestHandler<GetProyectoClasificadorListQuery, ProyectoClasificadorListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetProyectoClasificadorListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProyectoClasificadorListVm> Handle(GetProyectoClasificadorListQuery request, CancellationToken cancellationToken)
        {
            var ProyectoClasificadors = await _context.ProyectoClasificadors
                .Where(c => c.IDPROYECTO == request.Id)
                .OrderBy(e => e.IDPROYECTO)
                .Include(c => c.Clasificador)
                .ProjectTo<ProyectoClasificadorDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ProyectoClasificadorListVm
            {
                ProyectoClasificador = ProyectoClasificadors,
                Count = ProyectoClasificadors.Count
            };

            return vm;
        }


    }
}
