using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetListaSolicitudes
{
    public class GetListaSolicitudesQueryHandler : IRequestHandler<GetListaSolicitudesQuery, ListaSolicitudesVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetListaSolicitudesQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListaSolicitudesVm> Handle(GetListaSolicitudesQuery request, CancellationToken cancellationToken)
        {
            var solicitudes = await _context.SolicitudMasterData

                .OrderBy(p => p.FECHACREACION)
                .ProjectTo<SolicitudDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ListaSolicitudesVm
            {
                SolicitudesMasterData = solicitudes,
                count = solicitudes.Count
            };

            return vm;
        }
    }
}
