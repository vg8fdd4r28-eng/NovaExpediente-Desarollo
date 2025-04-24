using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.BasesDatos.Queries.GetListaBaseDatosConsulta
{
    public class GetBaseDatosConsultaListQueryHandler : IRequestHandler<GetBaseDatosConsultaListQuery, BaseDatosConsultaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetBaseDatosConsultaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BaseDatosConsultaListVm> Handle(GetBaseDatosConsultaListQuery request, CancellationToken cancellationToken)
        {
            var BaseDatos = await _context.BasesDatos
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<BaseDatosConsultaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new BaseDatosConsultaListVm
            {
                BasesDatos = BaseDatos,
                Count = BaseDatos.Count
            };

            return vm;
        }


    }
}
