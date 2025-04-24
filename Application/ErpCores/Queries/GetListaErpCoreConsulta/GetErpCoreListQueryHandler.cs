using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.ErpCores.Queries.GetListaErpCoreConsulta
{
    public class GetErpCoreConsultaListQueryHandler : IRequestHandler<GetErpCoreConsultaListQuery, ErpCoreConsultaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetErpCoreConsultaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ErpCoreConsultaListVm> Handle(GetErpCoreConsultaListQuery request, CancellationToken cancellationToken)
        {
            var ErpCore = await _context.ErpCores
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<ErpCoreConsultaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new ErpCoreConsultaListVm
            {
                ErpCores = ErpCore,
                Count = ErpCore.Count
            };

            return vm;
        }


    }
}
