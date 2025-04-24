using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.FormacionesAcademicas.Queries.GetListaFormacionAcademicaDocumento
{
    public class GetFormacionAcademicaDocumentoByIdQueryHandler : IRequestHandler<GetFormacionAcademicaDocumentoByIdQuery, FormacionAcademicaDocumentoVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetFormacionAcademicaDocumentoByIdQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FormacionAcademicaDocumentoVm> Handle(GetFormacionAcademicaDocumentoByIdQuery request, CancellationToken cancellationToken)
        {
            var formacionesAcademicas = await _context.FormacionesAcademicas
                .Where(c => c.IDFORMACIONACADEMICA == request.Id)
                .OrderByDescending(x => x.ANIO)
                .ThenBy(x => x.NOMBRE)
                .ProjectTo<FormacionAcademicaDocumentoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new FormacionAcademicaDocumentoVm
            {
                FormacionAcademica = formacionesAcademicas.FirstOrDefault()
            };

            return vm;
        }


    }
}
