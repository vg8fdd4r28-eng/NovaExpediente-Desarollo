using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Clouds.Queries.GetListaCloud
{
    public class GetCloudListQueryHandler : IRequestHandler<GetCloudListQuery, CloudListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCloudListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CloudListVm> Handle(GetCloudListQuery request, CancellationToken cancellationToken)
        {
            var Cloud = await _context.Cloudss
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<CloudDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CloudListVm
            {
                Clouds = Cloud,
                Count = Cloud.Count
            };

            return vm;
        }


    }
}
