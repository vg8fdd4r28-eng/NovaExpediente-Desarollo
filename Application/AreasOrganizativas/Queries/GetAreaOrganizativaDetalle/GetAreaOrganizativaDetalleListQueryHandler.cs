using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.AreasOrganizativas.Queries.GetAreaOrganizativaDetalle
{
    public class GetAreaOrganizativaDetalleListQueryHandler : IRequestHandler<GetAreaOrganizativaDetalleListQuery, AreaOrganizativaDetalleVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetAreaOrganizativaDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AreaOrganizativaDetalleVm> Handle(GetAreaOrganizativaDetalleListQuery request, CancellationToken cancellationToken)
        {
            var AreaOrganizativa = await _context.AreaOrganizativas
                .Where(c => c.IDAREAORGANIZATIVA == request.Id)
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<AreaOrganizativaDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new AreaOrganizativaDetalleVm
            {
                areaOrganizativa = AreaOrganizativa.FirstOrDefault()
            };

            return vm;
        }
    }
}
