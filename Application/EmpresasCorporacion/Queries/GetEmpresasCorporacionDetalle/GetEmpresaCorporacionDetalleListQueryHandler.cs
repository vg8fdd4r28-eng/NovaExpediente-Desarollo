using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.EmpresasCorporacion.Queries.GetEmpresasCorporacionDetalle
{
    public class GetEmpresaCorporacionDetalleListQueryHandler : IRequestHandler<GetEmpresaCorporacionDetalleListQuery, EmpresaCorporacionDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetEmpresaCorporacionDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmpresaCorporacionDetalleVm> Handle(GetEmpresaCorporacionDetalleListQuery request, CancellationToken cancellationToken)
        {
            var EmpresaCorporacion = await _context.EmpresaCorporacions
                .Where(e => e.IDEMPRESACORPORACION == request.Id)
                .OrderBy(e => e.NOMBRE)
                .ProjectTo<EmpresaCorporacionDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new EmpresaCorporacionDetalleVm
            {
                EmpresaCorporacion = EmpresaCorporacion.FirstOrDefault()
            };

            return vm;
        }


    }
}
