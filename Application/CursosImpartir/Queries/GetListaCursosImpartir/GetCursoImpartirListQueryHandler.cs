using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.CursosImpartir.Queries.GetListaCursosImpartir
{
    public class GetCursoImpartirListQueryHandler : IRequestHandler<GetCursoImpartirListQuery, CursoImpartirListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCursoImpartirListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CursoImpartirListVm> Handle(GetCursoImpartirListQuery request, CancellationToken cancellationToken)
        {
            var cursosImpartir = await _context.CursoImpartirs
                .Where(c => c.IDCOLABORADOR == request.Id)
                .ProjectTo<CursoImpartirDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CursoImpartirListVm
            {
                CursosImpartir = cursosImpartir,
                Count = cursosImpartir.Count
            };

            return vm;
        }


    }
}
