using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Afiliacion.Queries.AfiliacionDetalle;
using NovaExpediente.Application.Afiliacion.Queries.GetAfiliacionDetalle;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Afiliacion.Queries.GetListaAfiliacionDetalle
{
    public class GetAfiliacionDetalleListQueryHandler : IRequestHandler<GetAfiliacionDetalleListQuery, AfiliacionDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetAfiliacionDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AfiliacionDetalleVm> Handle(GetAfiliacionDetalleListQuery request, CancellationToken cancellationToken)
        {
            var afiliaciones = await _context.AfiliacionColaborador
                .Where(c => c.IDAFILIACIONCOLABORADOR == request.Id)
                .OrderByDescending(x => x.FECHAINICIO)
                .ThenBy(x => x.NOMBRE)
                .ProjectTo<AfiliacionColaboradorDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new AfiliacionDetalleVm
            {
                Afiliacion = afiliaciones.FirstOrDefault()
            };

            return vm;
        }


    }
}
