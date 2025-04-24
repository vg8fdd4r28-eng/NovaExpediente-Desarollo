using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Especialidades.Queries.GetListaEspecialidadConsulta
{
    public class GetEspecialidadConsultaListQueryHandler : IRequestHandler<GetEspecialidadConsultaListQuery, EspecialidadConsultaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetEspecialidadConsultaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EspecialidadConsultaListVm> Handle(GetEspecialidadConsultaListQuery request, CancellationToken cancellationToken)
        {
            var Especialidad = await _context.Especialidades
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<EspecialidadConsultaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new EspecialidadConsultaListVm
            {
                Especialidades = Especialidad,
                Count = Especialidad.Count
            };

            return vm;
        }


    }
}
