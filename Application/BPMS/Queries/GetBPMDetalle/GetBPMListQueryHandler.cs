using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;


namespace NovaExpediente.Application.BPMS.Queries.GetBPMDetalle
{
    class GetBPMDetalleListQueryHandler : IRequestHandler<GetBPMDetalleListQuery, BPMDetalleVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetBPMDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BPMDetalleVm> Handle(GetBPMDetalleListQuery request, CancellationToken cancellationToken)
        {
            var BPMs = await _context.BPMs
                .Where(c => c.IDBPM == request.Id)
                .OrderBy(c => c.Nombre)
                .ProjectTo<BPMDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new BPMDetalleVm
            {
                BPM = BPMs.FirstOrDefault()
            };

            return vm;
        }

    }
}
