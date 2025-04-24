using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.BasesDatos.Queries.GetListaBaseDatos
{
    public class GetBaseDatosListQueryHandler : IRequestHandler<GetBaseDatosListQuery, BaseDatosListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetBaseDatosListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BaseDatosListVm> Handle(GetBaseDatosListQuery request, CancellationToken cancellationToken)
        {
            var BaseDatos = await _context.BasesDatos
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<BaseDatosDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new BaseDatosListVm
            {
                BasesDatos = BaseDatos,
                Count = BaseDatos.Count
            };

            return vm;
        }


    }
}
