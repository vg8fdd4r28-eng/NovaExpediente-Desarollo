using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.TecnologiasProyectos.Queries.GetListaTecnologiaProyecto
{
    public class GetTecnologiaProyectoListQueryHandler : IRequestHandler<GetTecnologiaProyectoListQuery, TecnologiaProyectoListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetTecnologiaProyectoListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TecnologiaProyectoListVm> Handle(GetTecnologiaProyectoListQuery request, CancellationToken cancellationToken)
        {
            var TecnologiaProyectos = await _context.TecnologiaProyectos
                .Where(c => c.IDEXPERIENCIAPROYECTO == request.Id)
                .OrderBy(e => e.NOMBRE)
                .ProjectTo<TecnologiaProyectoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new TecnologiaProyectoListVm
            {
                TecnologiaProyectos = TecnologiaProyectos,
                Count = TecnologiaProyectos.Count
            };

            return vm;
        }


    }
}
