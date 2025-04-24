using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.AmbientesDesarrollo.Queries.GetListaAmbienteDesarrollo
{
    public class GetAmbienteDesarrolloListQueryHandler : IRequestHandler<GetAmbienteDesarrolloListQuery, AmbienteDesarrolloListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetAmbienteDesarrolloListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AmbienteDesarrolloListVm> Handle(GetAmbienteDesarrolloListQuery request, CancellationToken cancellationToken)
        {
            var AmbienteDesarrollo = await _context.AmbienteDesarrollos
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<AmbienteDesarrolloDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new AmbienteDesarrolloListVm
            {
                AmbientesDesarrollo = AmbienteDesarrollo,
                Count = AmbienteDesarrollo.Count
            };

            return vm;
        }


    }
}
