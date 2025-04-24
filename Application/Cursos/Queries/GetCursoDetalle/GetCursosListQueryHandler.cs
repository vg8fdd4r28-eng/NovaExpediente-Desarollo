using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Cursos.Queries.GetCursoDetalle
{
    public class GetCursoDetalleListQueryHandler : IRequestHandler<GetCursoDetalleListQuery, CursoDetalleVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCursoDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CursoDetalleVm> Handle(GetCursoDetalleListQuery request, CancellationToken cancellationToken)
        {
            var curso = await _context.Cursos
                .Where(c => c.IDCURSO == request.Id)
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<CursoDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CursoDetalleVm
            {
                Curso = curso.FirstOrDefault()
            };

            return vm;
        }
    }
}
