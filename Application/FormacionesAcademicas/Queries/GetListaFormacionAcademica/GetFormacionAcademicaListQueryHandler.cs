using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.FormacionesAcademicas.Queries.GetListaFormacionAcademica
{
    public class GetFormacionAcademicaListQueryHandler : IRequestHandler<GetFormacionAcademicaListQuery, FormacionAcademicaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetFormacionAcademicaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FormacionAcademicaListVm> Handle(GetFormacionAcademicaListQuery request, CancellationToken cancellationToken)
        {
            var formacionesAcademicas = await _context.FormacionesAcademicas
                .Where(c => c.IDCOLABORADOR == request.Id)
                .OrderByDescending(x => x.ANIO)
                .ThenBy(x => x.NOMBRE)
                .Include(c => c.CentroEstudio)
                .Include(c => c.GradoAcademico)
                .ProjectTo<FormacionAcademicaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new FormacionAcademicaListVm
            {
                FormacionesAcademicas = formacionesAcademicas,
                Count = formacionesAcademicas.Count
            };

            return vm;
        }


    }
}
