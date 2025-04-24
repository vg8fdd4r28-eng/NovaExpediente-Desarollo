using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.CursosColaborador.Queries.GetListaCursosColaboradorAdjunto
{
    public class GetCursoColaboradorAdjuntoListQueryHandler : IRequestHandler<GetCursoColaboradorAdjuntoListQuery, CursoColaboradorAdjuntoListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCursoColaboradorAdjuntoListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CursoColaboradorAdjuntoListVm> Handle(GetCursoColaboradorAdjuntoListQuery request, CancellationToken cancellationToken)
        {
            var cursosColaborador = await _context.CursoColaboradors
                .Where(c => c.IDCOLABORADOR == request.Id)
                .OrderBy(x => x.NOMBRE)
                .ProjectTo<CursoColaboradorAdjuntoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CursoColaboradorAdjuntoListVm
            {
                CursosColaborador = cursosColaborador,
                Count = cursosColaborador.Count
            };

            return vm;
        }


    }
}
