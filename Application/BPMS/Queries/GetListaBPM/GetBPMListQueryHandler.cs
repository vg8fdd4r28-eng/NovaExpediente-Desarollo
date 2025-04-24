using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;


namespace NovaExpediente.Application.BPMS.Queries.GetListaBPM
{
    class GetBPMListQueryHandler : IRequestHandler<GetBPMListQuery, BPMListVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetBPMListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BPMListVm> Handle(GetBPMListQuery request, CancellationToken cancellationToken)
        {
            var BPMs = await _context.BPMs
                .OrderBy(c => c.Nombre)
                .ProjectTo<BPMDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new BPMListVm
            {
                BPMs = BPMs,
                Count = BPMs.Count
            };

            return vm;
        }

    }
}
