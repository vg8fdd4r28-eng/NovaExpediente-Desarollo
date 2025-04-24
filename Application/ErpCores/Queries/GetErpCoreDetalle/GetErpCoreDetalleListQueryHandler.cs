using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ErpCores.Queries.GetErpCoreDetalle
{
    public class GetErpCoreDetalleListQueryHandler : IRequestHandler<GetErpCoreDetalleListQuery, ErpCoreDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetErpCoreDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ErpCoreDetalleVm> Handle(GetErpCoreDetalleListQuery request, CancellationToken cancellationToken)
        {
            var ErpCore = await _context.ErpCores
                .Where(c => c.IDERPCORE == request.Id)
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<ErpCoreDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new ErpCoreDetalleVm
            {
                ErpCore = ErpCore.FirstOrDefault()
            };

            return vm;
        }


    }
}
