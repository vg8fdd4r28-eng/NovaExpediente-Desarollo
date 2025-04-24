using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.LenguajesProgramacion.Queries.GetLenguajeProgramacionDetalle
{
    public class GetLenguajeProgramacionDetalleListQueryHandler : IRequestHandler<GetLenguajeProgramacionDetalleListQuery, LenguajeProgramacionDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetLenguajeProgramacionDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LenguajeProgramacionDetalleVm> Handle(GetLenguajeProgramacionDetalleListQuery request, CancellationToken cancellationToken)
        {
            var LenguajeProgramacion = await _context.LenguajeProgramacions
                .Where(c => c.IDLENGUAJEPROGRAMACION == request.Id)
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<LenguajeProgramacionDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new LenguajeProgramacionDetalleVm
            {
                LenguajeProgramacion = LenguajeProgramacion.FirstOrDefault()
            };

            return vm;
        }


    }
}
