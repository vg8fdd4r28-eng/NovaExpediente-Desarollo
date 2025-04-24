using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Empresas.Queries.GetListaEmpresas
{
    public class GetEmpresaListQueryHandler : IRequestHandler<GetEmpresaListQuery, EmpresaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetEmpresaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmpresaListVm> Handle(GetEmpresaListQuery request, CancellationToken cancellationToken)
        {
            var Empresa = await _context.Empresas
                .OrderBy(e => e.NOMBRE)
                .ProjectTo<EmpresaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new EmpresaListVm
            {
                Empresas = Empresa,
                Count = Empresa.Count
            };

            return vm;
        }


    }
}
