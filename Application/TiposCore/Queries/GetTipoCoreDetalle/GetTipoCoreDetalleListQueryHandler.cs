using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.TiposCore.Queries.GetTipoCoreDetalle
{
    public class GetTipoCoreDetalleListQueryHandler : IRequestHandler<GetTipoCoreDetalleListQuery, TipoCoreDetalleVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetTipoCoreDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TipoCoreDetalleVm> Handle(GetTipoCoreDetalleListQuery request, CancellationToken cancellationToken)
        {
            var tipoCore = await _context.TipoCores
                .Where(p => p.IDTIPOCORE == request.Id)
                .OrderBy(p => p.NOMBRE)
                .ProjectTo<TiposCoreDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new TipoCoreDetalleVm
            {
                TipoCore = tipoCore.FirstOrDefault()
            };

            return vm;
        }
    }
}
