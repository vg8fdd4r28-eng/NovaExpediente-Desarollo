using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.BasesDatos.Queries.GetBaseDatosDetalle
{
    public class GetBaseDatosDetalleListQueryHandler : IRequestHandler<GetBaseDatosDetalleListQuery, BaseDatosDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetBaseDatosDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BaseDatosDetalleVm> Handle(GetBaseDatosDetalleListQuery request, CancellationToken cancellationToken)
        {
            var BaseDatos = await _context.BasesDatos
                .Where(c => c.IDBASEDATOS == request.Id)
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<BaseDatosDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new BaseDatosDetalleVm
            {
                BaseDatos = BaseDatos.FirstOrDefault()
            };

            return vm;
        }


    }
}
