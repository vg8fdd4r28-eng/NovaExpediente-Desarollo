using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Incapacidades.Queries.GetCargaIncapacidad
{
    public class GetCargaIncapacidadListQueryHandler : IRequestHandler<GetCargaIncapacidadListQuery, CargaIncapacidadListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCargaIncapacidadListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CargaIncapacidadListVm> Handle(GetCargaIncapacidadListQuery request, CancellationToken cancellationToken)
        {
            var cargas = await _context.CargaIncapacidadess

                .OrderBy(p => p.IDCARGAINCAPACIDADES)
                .ProjectTo<CargaIncapacidadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CargaIncapacidadListVm
            {
                Cargas = cargas,
                count = cargas.Count
            };

            return vm;
        }
    }
}
