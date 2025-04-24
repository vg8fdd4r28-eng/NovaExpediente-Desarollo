using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Distritos.Queries.GetDistritoDetalle
{
    public class GetDistritoDetalleListQueryHandler : IRequestHandler<GetDistritoDetalleListQuery, DistritoDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetDistritoDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DistritoDetalleVm> Handle(GetDistritoDetalleListQuery request, CancellationToken cancellationToken)
        {
            var distritos = await _context.Distritos
                .Where(c => c.IDDISTRITO == request.Id)
                .OrderBy(c => c.NOMBRE)
                .Include(c => c.Canton)
                .Include(c => c.Canton.Provincia)
                .ProjectTo<DistritoDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new DistritoDetalleVm
            {
                Distrito = distritos.FirstOrDefault()
            };

            return vm;
        }


    }
}
