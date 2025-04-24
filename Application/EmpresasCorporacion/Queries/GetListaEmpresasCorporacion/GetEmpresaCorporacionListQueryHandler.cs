using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.EmpresasCorporacion.Queries.GetListaEmpresasCorporacion
{
    public class GetEmpresaCorporacionListQueryHandler : IRequestHandler<GetEmpresaCorporacionListQuery, EmpresaCorporacionListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetEmpresaCorporacionListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmpresaCorporacionListVm> Handle(GetEmpresaCorporacionListQuery request, CancellationToken cancellationToken)
        {
            var EmpresaCorporacion = await _context.EmpresaCorporacions
                .OrderBy(e => e.NOMBRE)
                .ProjectTo<EmpresaCorporacionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new EmpresaCorporacionListVm
            {
                EmpresaCorporacions = EmpresaCorporacion,
                Count = EmpresaCorporacion.Count
            };

            return vm;
        }


    }
}
