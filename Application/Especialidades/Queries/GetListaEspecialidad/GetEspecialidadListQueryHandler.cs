using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NovaExpediente.Application.Especialidades.Queries.GetListaEspecialidad
{
    public class GetEspecialidadListQueryHandler : IRequestHandler<GetEspecialidadListQuery, EspecialidadListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetEspecialidadListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EspecialidadListVm> Handle(GetEspecialidadListQuery request, CancellationToken cancellationToken)
        {
            var Especialidad = await _context.Especialidades
                .OrderBy(c => c.NOMBRE)
                .ProjectTo<EspecialidadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);            

            var vm = new EspecialidadListVm
            {
                Especialidades = Especialidad,
                Count = Especialidad.Count
            };

            return vm;
        }


    }
}
