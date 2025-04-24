using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ColaboradorProyectos.Queries.GetListaColaboradorProyectos
{
    public class GetColaboradorProyectoListQueryHandler : IRequestHandler<GetColaboradorProyectoListQuery, ColaboradorProyectoListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetColaboradorProyectoListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ColaboradorProyectoListVm> Handle(GetColaboradorProyectoListQuery request, CancellationToken cancellationToken)
        {
            var ColaboradorProyectos = await _context.ColaboradorProyectos
                .Where(c => c.IDCOLABORADOR == request.Id)
                .OrderBy(e => e.IDCOLABORADORPROYECTO)
                .Include(c => c.Proyecto)
                .Include(c => c.Puesto)
                .Include(c => c.Colaborador)
                .ProjectTo<ColaboradorProyectoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ColaboradorProyectoListVm
            {
                ColaboradorProyectos = ColaboradorProyectos,
                Count = ColaboradorProyectos.Count
            };

            return vm;
        }


    }
}
