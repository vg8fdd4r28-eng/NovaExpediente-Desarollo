using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.EspecialidadesConsultor.Queries.GetListaEspecialidadConsultor
{
    public class GetEspecialidadConsultorListQueryHandler : IRequestHandler<GetEspecialidadConsultorListQuery, EspecialidadConsultorListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetEspecialidadConsultorListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EspecialidadConsultorListVm> Handle(GetEspecialidadConsultorListQuery request, CancellationToken cancellationToken)
        {
            var especialidadesConsultor = await _context.EspecialidadConsultors
                .Where(c => c.IDCOLABORADOR == request.Id)
                .Select(e => e)
                .OrderByDescending(e => e.EXPERIENCIA)
                .ThenBy(e => e.Especialidad.NOMBRE)
                .Include(c => c.Especialidad)
                .ProjectTo<EspecialidadConsultorDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new EspecialidadConsultorListVm
            {
                EspecialidadesConsultor = especialidadesConsultor,
                Count = especialidadesConsultor.Count
            };

            return vm;
        }


    }
}
