using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;


namespace NovaExpediente.Application.TiposCore.Queries.GetListaTiposCore
{
    public class GetTipoCoreListQueryHandler : IRequestHandler<GetTipoCoreListQuery, TipoCoreListVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetTipoCoreListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TipoCoreListVm> Handle(GetTipoCoreListQuery request, CancellationToken cancellationToken)
        {
            var tipoCore = await _context.TipoCores

                .OrderBy(p => p.NOMBRE)
                .ProjectTo<TiposCoreDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new TipoCoreListVm
            {
                TiposCores = tipoCore,
                Count = tipoCore.Count
            };

            return vm;
        }
    }
}
