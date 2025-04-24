using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradorDM
{
    public class GetColaboradorDMListQueryHandler : IRequestHandler<GetColaboradorDMListQuery, ColaboradorDMListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetColaboradorDMListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ColaboradorDMListVm> Handle(GetColaboradorDMListQuery request, CancellationToken cancellationToken)
        {
            var ColaboradoresDM = await _context.Colaboradores
                .Where(c => c.ESDELIVERYMANAGER == true && c.ACTIVO == true)
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<ColaboradorDMDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ColaboradorDMListVm
            {
                ColaboradoresDM = ColaboradoresDM,
                Count = ColaboradoresDM.Count
            };

            return vm;
        }


    }
}
