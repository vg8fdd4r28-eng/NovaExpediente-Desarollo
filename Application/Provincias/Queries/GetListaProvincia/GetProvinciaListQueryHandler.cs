using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Provincias.Queries.GetListaProvincia
{
    public class GetProvinciaListQueryHandler : IRequestHandler<GetProvinciaListQuery, ProvinciaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetProvinciaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProvinciaListVm> Handle(GetProvinciaListQuery request, CancellationToken cancellationToken)
        {
            var BusConsulta = await _context.Provincias
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<ProvinciaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ProvinciaListVm
            {
                Provincias = BusConsulta,
                Count = BusConsulta.Count
            };

            return vm;
        }


    }
}
