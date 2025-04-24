using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.AreasOrganizativas.Queries.GetListaAreaOrganizativa
{
    public class GetAreaOrganizativaListQueryHandler : IRequestHandler<GetAreaOrganizativaListQuery, AreaOrganizativaListVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetAreaOrganizativaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AreaOrganizativaListVm> Handle(GetAreaOrganizativaListQuery request, CancellationToken cancellationToken)
        {
            var AreaOrganizativa = await _context.AreaOrganizativas
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<AreaOrganizativaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new AreaOrganizativaListVm
            {
                areaOrganizativas = AreaOrganizativa,
                Count = AreaOrganizativa.Count
            };

            return vm;
        }
    }
}
