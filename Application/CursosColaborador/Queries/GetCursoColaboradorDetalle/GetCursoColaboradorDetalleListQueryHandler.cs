using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.CursosColaborador.Queries.GetCursoColaboradorDetalle
{
    public class GetCursoColaboradorDetalleListQueryHandler : IRequestHandler<GetCursoColaboradorDetalleListQuery, CursoColaboradorDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCursoColaboradorDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CursoColaboradorDetalleVm> Handle(GetCursoColaboradorDetalleListQuery request, CancellationToken cancellationToken)
        {
            var cursosColaborador = await _context.CursoColaboradors
                .Where(c => c.IDCURSOCOLABORADOR == request.Id)
                .OrderBy(x => x.NOMBRE)
                .ProjectTo<CursoColaboradorDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CursoColaboradorDetalleVm
            {
                CursoColaborador = cursosColaborador.FirstOrDefault()
            };

            return vm;
        }


    }
}
