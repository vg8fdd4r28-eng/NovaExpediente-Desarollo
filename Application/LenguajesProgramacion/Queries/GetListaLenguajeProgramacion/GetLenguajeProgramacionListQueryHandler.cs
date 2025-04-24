using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.LenguajesProgramacion.Queries.GetListaLenguajeProgramacion
{
    public class GetLenguajeProgramacionListQueryHandler : IRequestHandler<GetLenguajeProgramacionListQuery, LenguajeProgramacionListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetLenguajeProgramacionListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LenguajeProgramacionListVm> Handle(GetLenguajeProgramacionListQuery request, CancellationToken cancellationToken)
        {
            var LenguajeProgramacion = await _context.LenguajeProgramacions
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<LenguajeProgramacionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new LenguajeProgramacionListVm
            {
                LenguajesProgramacion = LenguajeProgramacion,
                Count = LenguajeProgramacion.Count
            };

            return vm;
        }


    }
}
