using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.AmbientesDesarrollo.Queries.GetAmbienteDesarrolloDetalle
{
    public class GetAmbienteDesarrolloDetalleListQueryHandler : IRequestHandler<GetAmbienteDesarrolloDetalleListQuery, AmbienteDesarrolloDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetAmbienteDesarrolloDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AmbienteDesarrolloDetalleVm> Handle(GetAmbienteDesarrolloDetalleListQuery request, CancellationToken cancellationToken)
        {
            var AmbienteDesarrollo = await _context.AmbienteDesarrollos
                .Where(c => c.IDAMBIENTEDESARROLLO == request.Id)
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<AmbienteDesarrolloDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);


            var vm = new AmbienteDesarrolloDetalleVm
            {
                AmbienteDesarrollo = AmbienteDesarrollo.FirstOrDefault()
            };

            return vm;
        }


    }
}
