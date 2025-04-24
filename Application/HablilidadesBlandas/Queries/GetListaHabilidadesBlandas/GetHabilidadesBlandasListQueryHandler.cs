using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.HablilidadesBlandas.Queries.GetListaHabilidadesBlandas
{
    public class GetHabilidadesBlandasListQueryHandler : IRequestHandler<GetHabilidadesBlandasListQuery, HabilidadesBlandasListVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetHabilidadesBlandasListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<HabilidadesBlandasListVm> Handle(GetHabilidadesBlandasListQuery request, CancellationToken cancellationToken)
        {
            var habilidadesBlandas = await _context.HabilidadBlandas
                .ProjectTo<HabilidadesBlandasDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new HabilidadesBlandasListVm
            {
                habilidadesBlandas = habilidadesBlandas,
                Count = habilidadesBlandas.Count
            };

            return vm;
        }
    }
}
