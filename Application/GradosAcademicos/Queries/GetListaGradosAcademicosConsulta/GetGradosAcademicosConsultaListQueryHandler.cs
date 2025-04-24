using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.GradosAcademicos.Queries.GetListaGradosAcademicosConsulta
{
    public class GetGradosAcademicosConsultaListQueryHandler : IRequestHandler<GetGradosAcademicosConsultaListQuery, GradosAcademicosConsultaListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetGradosAcademicosConsultaListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GradosAcademicosConsultaListVm> Handle(GetGradosAcademicosConsultaListQuery request, CancellationToken cancellationToken)
        {
            var gradosAcademicos = await _context.GradosAcademicos
                .ProjectTo<GradoAcademicoConsultaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new GradosAcademicosConsultaListVm
            {
                GradosAcademicos = gradosAcademicos,
                Count = gradosAcademicos.Count
            };

            return vm;
        }
    }
}