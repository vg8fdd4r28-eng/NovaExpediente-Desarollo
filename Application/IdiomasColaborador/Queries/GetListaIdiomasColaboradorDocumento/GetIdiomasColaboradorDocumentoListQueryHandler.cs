using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.IdiomasColaborador.Queries.GetListaIdiomasColaboradorDocumento
{
    public class GetIdiomasColaboradorDocumentoListQueryHandler : IRequestHandler<GetIdiomasColaboradorDocumentoListQuery, IdiomaColaboradorDocumentoVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetIdiomasColaboradorDocumentoListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IdiomaColaboradorDocumentoVm> Handle(GetIdiomasColaboradorDocumentoListQuery request, CancellationToken cancellationToken)
        {
            string[] pos = request.Id.Split("-");

            var idiomasConsultor = await _context.IdiomaColaboradors
                .Where(c => c.IDIDIOMA == int.Parse(pos[0]))
                .Where(c => c.IDCOLABORADOR == int.Parse(pos[1]))
                .ProjectTo<IdiomaColaboradorDocumentoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new IdiomaColaboradorDocumentoVm
            {
                IdiomasColaborador = idiomasConsultor.FirstOrDefault()
            };

            return vm;
        }


    }
}
