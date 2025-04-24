using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Tecnologias.Queries.GetTecnologiaDetalle
{
    public class GetTecnologiaDetalleListQueryHandler : IRequestHandler<GetTecnologiaDetalleListQuery, TecnologiaDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetTecnologiaDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TecnologiaDetalleVm> Handle(GetTecnologiaDetalleListQuery request, CancellationToken cancellationToken)
        {
            var Tecnologia = await _context.Tecnologias
                .Where(c => c.IDTECNOLOGIA == request.Id)
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<TecnologiaDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new TecnologiaDetalleVm
            {
                Tecnologia = Tecnologia.FirstOrDefault()
            };

            return vm;
        }


    }
}
