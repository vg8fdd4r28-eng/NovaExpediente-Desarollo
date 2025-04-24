using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.CentrosEstudios.Queries.GetCentroEstudioDetalle
{
    public class GetCentroEstudioDetalleListQueryHandler : IRequestHandler<GetCentroEstudioDetalleListQuery, CentroEstudioDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCentroEstudioDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CentroEstudioDetalleVm> Handle(GetCentroEstudioDetalleListQuery request, CancellationToken cancellationToken)
        {
            var CentrosEstudios = await _context.CentroEstudios
                .Where(c => c.IDCENTROESTUDIO == request.Id)
                .OrderBy(c => c.Nombre)
                .ProjectTo<CentroEstudioDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CentroEstudioDetalleVm
            {
                CentroEstudio = CentrosEstudios.FirstOrDefault()
            };

            return vm;
        }
    }
}
