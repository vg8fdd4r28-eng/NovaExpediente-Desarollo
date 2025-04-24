using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.FormacionesAcademicas.Queries.GetListaFormacionAcademicaAdjunto
{
    public class GetFormacionAcademicaAdjuntoListQueryHandler : IRequestHandler<GetFormacionAcademicaAdjuntoListQuery, FormacionAcademicaAdjuntoListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetFormacionAcademicaAdjuntoListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FormacionAcademicaAdjuntoListVm> Handle(GetFormacionAcademicaAdjuntoListQuery request, CancellationToken cancellationToken)
        {
            var formacionesAcademicas = await _context.FormacionesAcademicas
                .Where(c => c.IDCOLABORADOR == request.Id)
                .OrderByDescending(x => x.ANIO)
                .ThenBy(x => x.NOMBRE)
                .ProjectTo<FormacionAcademicaAdjuntoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new FormacionAcademicaAdjuntoListVm
            {
                FormacionesAcademicas = formacionesAcademicas,
                Count = formacionesAcademicas.Count
            };

            return vm;
        }


    }
}
