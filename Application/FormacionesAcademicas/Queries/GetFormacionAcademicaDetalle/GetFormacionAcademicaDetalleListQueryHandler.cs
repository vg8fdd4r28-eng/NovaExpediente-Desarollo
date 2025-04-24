using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.FormacionesAcademicas.Queries.GetListaFormacionAcademicaDetalle
{
    public class GetFormacionAcademicaDetalleListQueryHandler : IRequestHandler<GetFormacionAcademicaDetalleListQuery, FormacionAcademicaDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetFormacionAcademicaDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FormacionAcademicaDetalleVm> Handle(GetFormacionAcademicaDetalleListQuery request, CancellationToken cancellationToken)
        {
            var formacionesAcademicas = await _context.FormacionesAcademicas
                .Where(c => c.IDFORMACIONACADEMICA == request.Id)
                .OrderByDescending(x => x.ANIO)
                .ThenBy(x => x.NOMBRE)
                .Include(c => c.CentroEstudio)
                .Include(c => c.GradoAcademico)
                .ProjectTo<FormacionAcademicaDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new FormacionAcademicaDetalleVm
            {
                FormacionAcademica = formacionesAcademicas.FirstOrDefault()
            };

            return vm;
        }


    }
}
