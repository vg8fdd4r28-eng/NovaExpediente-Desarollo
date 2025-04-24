using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.IdiomasColaborador.Queries.GetListaIdiomasColaborador
{
    public class GetIdiomasColaboradorListQueryHandler : IRequestHandler<GetIdiomasColaboradorListQuery, IdiomaColaboradorListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetIdiomasColaboradorListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IdiomaColaboradorListVm> Handle(GetIdiomasColaboradorListQuery request, CancellationToken cancellationToken)
        {
            var idiomasConsultor = await _context.IdiomaColaboradors
                .Where(c => c.IDCOLABORADOR == request.Id)
                .Include(c => c.Idioma)
                .Include(c => c.NivelIdioma)
                .ProjectTo<IdiomaColaboradorDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new IdiomaColaboradorListVm
            {
                IdiomasColaborador = idiomasConsultor,
                Count = idiomasConsultor.Count
            };

            return vm;
        }


    }
}
