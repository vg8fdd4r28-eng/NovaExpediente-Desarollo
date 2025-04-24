using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ProyectoTecnologias.Queries.GetListaProyectoTecnologia
{
    public class GetProyectoTecnologiaListQueryHandler : IRequestHandler<GetProyectoTecnologiaListQuery, ProyectoTecnologiaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetProyectoTecnologiaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProyectoTecnologiaListVm> Handle(GetProyectoTecnologiaListQuery request, CancellationToken cancellationToken)
        {
            var ProyectoTecnologias = await _context.ProyectoTecnologias
                .Where(c => c.IDPROYECTO == request.Id)
                .OrderBy(e => e.IDPROYECTO)
                .Include(c => c.Tecnologia)
                .ProjectTo<ProyectoTecnologiaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ProyectoTecnologiaListVm
            {
                ProyectoTecnologia = ProyectoTecnologias,
                Count = ProyectoTecnologias.Count
            };

            return vm;
        }


    }
}
