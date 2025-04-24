using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.GradosAcademicos.Queries.GetListaGradosAcademicos
{
    public class GetGradosAcademicosListQueryHandler : IRequestHandler<GetGradosAcademicosListQuery, GradosAcademicosListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetGradosAcademicosListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GradosAcademicosListVm> Handle(GetGradosAcademicosListQuery request, CancellationToken cancellationToken)
        {
            var gradosAcademicos = await _context.GradosAcademicos
                .ProjectTo<GradoAcademicoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new GradosAcademicosListVm
            {
                GradosAcademicos = gradosAcademicos,
                Count = gradosAcademicos.Count
            };

            return vm;
        }
    }
}