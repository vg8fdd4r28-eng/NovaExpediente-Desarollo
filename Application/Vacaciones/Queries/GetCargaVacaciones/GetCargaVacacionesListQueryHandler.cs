using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Vacaciones.Queries.GetCargaVacaciones
{
    public class GetCargaVacacionesListQueryHandler : IRequestHandler<GetCargaVacacionesListQuery, CargaVacacionesListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCargaVacacionesListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CargaVacacionesListVm> Handle(GetCargaVacacionesListQuery request, CancellationToken cancellationToken)
        {
            var cargas = await _context.CargaVacacioness

                .OrderBy(p => p.IDCARGAVACACIONES)
                .ProjectTo<CargaVacacionesDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CargaVacacionesListVm
            {
                Cargas = cargas,
                count = cargas.Count
            };

            return vm;
        }
    }
}
