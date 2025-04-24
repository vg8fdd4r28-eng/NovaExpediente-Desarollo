using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.IdiomasColaborador.Queries.GetListaIdiomasColaboradorAdjunto
{
    public class GetIdiomasColaboradorAdjuntoListQueryHandler : IRequestHandler<GetIdiomasColaboradorAdjuntoListQuery, IdiomaColaboradorAdjuntoListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetIdiomasColaboradorAdjuntoListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IdiomaColaboradorAdjuntoListVm> Handle(GetIdiomasColaboradorAdjuntoListQuery request, CancellationToken cancellationToken)
        {
            var idiomasConsultor = await _context.IdiomaColaboradors
                .Where(c => c.IDCOLABORADOR == request.Id)
                .Include(c => c.Idioma)
                .Include(c => c.NivelIdioma)
                .ProjectTo<IdiomaColaboradorAdjuntoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new IdiomaColaboradorAdjuntoListVm
            {
                IdiomasColaborador = idiomasConsultor,
                Count = idiomasConsultor.Count
            };

            return vm;
        }


    }
}
