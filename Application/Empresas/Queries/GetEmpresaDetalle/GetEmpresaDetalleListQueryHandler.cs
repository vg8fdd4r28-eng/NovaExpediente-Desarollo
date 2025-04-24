using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Empresas.Queries.GetEmpresaDetalle
{
    public class GetEmpresaDetalleListQueryHandler : IRequestHandler<GetEmpresaDetalleListQuery, EmpresaDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetEmpresaDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmpresaDetalleVm> Handle(GetEmpresaDetalleListQuery request, CancellationToken cancellationToken)
        {
            var Empresa = await _context.Empresas
                .Where(e => e.IDEMPRESA == request.Id)
                .OrderBy(e => e.NOMBRE)
                .ProjectTo<EmpresaDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new EmpresaDetalleVm
            {
                Empresa = Empresa.FirstOrDefault()
            };

            return vm;
        }


    }
}
