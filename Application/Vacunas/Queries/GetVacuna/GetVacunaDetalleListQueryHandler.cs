
    
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Vacunas.Queries.GetVacunaDetalle
{
    public class GetVacunaDetalleListQueryHandler : IRequestHandler<GetVacunaDetalleListQuery, VacunaDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetVacunaDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VacunaDetalleVm> Handle(GetVacunaDetalleListQuery request, CancellationToken cancellationToken)
        {
            var _Vacuna = await _context.Vacunas
                .Where(c => c.IdVacuna == request.IdVacuna)
                .OrderBy(x => x.IdVacuna)
                .ProjectTo<VacunaDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new VacunaDetalleVm
            {
                Vacuna = _Vacuna.FirstOrDefault()
            };

            return vm;
        }


    }
}
