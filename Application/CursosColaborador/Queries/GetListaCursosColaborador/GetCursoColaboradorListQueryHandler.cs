using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.CursosColaborador.Queries.GetListaCursosColaborador
{
    public class GetCursoColaboradorListQueryHandler : IRequestHandler<GetCursoColaboradorListQuery, CursoColaboradorListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCursoColaboradorListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CursoColaboradorListVm> Handle(GetCursoColaboradorListQuery request, CancellationToken cancellationToken)
        {
            var cursosColaborador = await _context.CursoColaboradors
                .Where(c => c.IDCOLABORADOR == request.Id)
                .OrderBy(x => x.NOMBRE)
                .ProjectTo<CursoColaboradorDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CursoColaboradorListVm
            {
                CursosColaborador = cursosColaborador,
                Count = cursosColaborador.Count
            };

            return vm;
        }


    }
}
