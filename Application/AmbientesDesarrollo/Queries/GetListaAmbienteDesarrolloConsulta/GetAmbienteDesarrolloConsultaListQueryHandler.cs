using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.AmbientesDesarrollo.Queries.GetListaAmbienteDesarrolloConsulta
{
    public class GetAmbienteDesarrolloConsultaListQueryHandler : IRequestHandler<GetAmbienteDesarrolloConsultaListQuery, AmbienteDesarrolloConsultaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetAmbienteDesarrolloConsultaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AmbienteDesarrolloConsultaListVm> Handle(GetAmbienteDesarrolloConsultaListQuery request, CancellationToken cancellationToken)
        {
            var AmbienteDesarrollo = await _context.AmbienteDesarrollos
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<AmbienteDesarrolloConsultaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new AmbienteDesarrolloConsultaListVm
            {
                AmbientesDesarrollo = AmbienteDesarrollo,
                Count = AmbienteDesarrollo.Count
            };

            return vm;
        }


    }
}
