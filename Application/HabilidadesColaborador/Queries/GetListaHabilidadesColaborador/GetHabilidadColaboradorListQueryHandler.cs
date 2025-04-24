using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.HabilidadesColaborador.Queries.GetListaHabilidadesColaborador
{
    public class GetHabilidadColaboradorListQueryHandler : IRequestHandler<GetHabilidadColaboradorListQuery, HabilidadColaboradorListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetHabilidadColaboradorListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<HabilidadColaboradorListVm> Handle(GetHabilidadColaboradorListQuery request, CancellationToken cancellationToken)
        {
            var habilidadesConsultor = await _context.HabilidadColaboradors
                .Where(c => c.IDCOLABORADOR == request.Id)
                .OrderBy(c => c.HabilidadBlanda.NOMBRE)
                .ProjectTo<HabilidadColaboradorDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new HabilidadColaboradorListVm
            {
                HabilidadesColaborador = habilidadesConsultor,
                Count = habilidadesConsultor.Count
            };

            return vm;
        }


    }
}
