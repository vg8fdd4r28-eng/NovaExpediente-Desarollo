using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Clasificadores.Queries.GetListaClasificadores
{
    public class GetClasificadoresListQueryHandler : IRequestHandler<GetClasificadoresListQuery, ClasificadoresListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetClasificadoresListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClasificadoresListVm> Handle(GetClasificadoresListQuery request, CancellationToken cancellationToken)
        {
            var clasificadores = await _context.Clasificadores
                .ProjectTo<ClasificadoresDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ClasificadoresListVm
            {
                Clasificadores = clasificadores,
                Count = clasificadores.Count
            };

            return vm;
        }


    }
}
