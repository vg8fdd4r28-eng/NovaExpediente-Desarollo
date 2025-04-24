using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.CentrosEstudios.Queries.GetListaCentroEstudio
{
    public class GetCentroEstudioListQueryHandler : IRequestHandler<GetCentroEstudioListQuery, CentroEstudioListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCentroEstudioListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CentroEstudioListVm> Handle(GetCentroEstudioListQuery request, CancellationToken cancellationToken)
        {
            var CentrosEstudios = await _context.CentroEstudios
                .OrderBy(c => c.Nombre)
                .ProjectTo<CentroEstudioDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CentroEstudioListVm
            {
                CentrosEstudios = CentrosEstudios,
                Count = CentrosEstudios.Count
            };

            return vm;
        }
    }
}
