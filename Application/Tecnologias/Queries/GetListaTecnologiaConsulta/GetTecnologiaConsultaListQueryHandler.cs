using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Tecnologias.Queries.GetListaTecnologiaConsulta
{
    public class GetTecnologiaConsultaListQueryHandler : IRequestHandler<GetTecnologiaConsultaListQuery, TecnologiaConsultaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetTecnologiaConsultaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TecnologiaConsultaListVm> Handle(GetTecnologiaConsultaListQuery request, CancellationToken cancellationToken)
        {
            var Tecnologia = await _context.Tecnologias
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<TecnologiaConsultaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new TecnologiaConsultaListVm
            {
                Tecnologias = Tecnologia,
                Count = Tecnologia.Count
            };

            return vm;
        }


    }
}
