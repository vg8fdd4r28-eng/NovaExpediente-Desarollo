using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Tecnologias.Queries.GetListaTecnologia
{
    public class GetTecnologiaListQueryHandler : IRequestHandler<GetTecnologiaListQuery, TecnologiaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetTecnologiaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TecnologiaListVm> Handle(GetTecnologiaListQuery request, CancellationToken cancellationToken)
        {
            var Tecnologia = await _context.Tecnologias
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<TecnologiaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new TecnologiaListVm
            {
                Tecnologias = Tecnologia,
                Count = Tecnologia.Count
            };

            return vm;
        }


    }
}
