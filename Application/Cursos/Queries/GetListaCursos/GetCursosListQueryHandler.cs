using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Cursos.Queries.GetListaCursos
{
    public class GetCursosListQueryHandler : IRequestHandler<GetCursosListQuery, CursosListVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCursosListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CursosListVm> Handle(GetCursosListQuery request, CancellationToken cancellationToken)
        {
            var cursos = await _context.Cursos
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<CursosDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CursosListVm
            {
                Cursos = cursos,
                Count = cursos.Count
            };

            return vm;
        }
    }
}
