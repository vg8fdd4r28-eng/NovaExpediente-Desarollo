using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.LenguajesProgramacion.Queries.GetListaLenguajeProgramacionConsulta
{
    public class GetLenguajeProgramacionConsultaListQueryHandler : IRequestHandler<GetLenguajeProgramacionConsultaListQuery, LenguajeProgramacionConsultaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetLenguajeProgramacionConsultaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LenguajeProgramacionConsultaListVm> Handle(GetLenguajeProgramacionConsultaListQuery request, CancellationToken cancellationToken)
        {
            var LenguajeProgramacion = await _context.LenguajeProgramacions
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<LenguajeProgramacionConsultaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new LenguajeProgramacionConsultaListVm
            {
                LenguajesProgramacion = LenguajeProgramacion,
                Count = LenguajeProgramacion.Count
            };

            return vm;
        }


    }
}
