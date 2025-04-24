using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.BPMS.Queries.GetListaBPMConsulta
{
    public class GetBPMConsultaListQueryHandler : IRequestHandler<GetBPMConsultaListQuery, BPMConsultaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetBPMConsultaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BPMConsultaListVm> Handle(GetBPMConsultaListQuery request, CancellationToken cancellationToken)
        {
            var BPMConsulta = await _context.BPMs
                .OrderBy(c => c.Nombre)
                .ProjectTo<BPMConsultaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new BPMConsultaListVm
            {
                BPMS = BPMConsulta,
                Count = BPMConsulta.Count
            };

            return vm;
        }


    }
}
