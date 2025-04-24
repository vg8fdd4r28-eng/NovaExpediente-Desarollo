using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.CurriculumVitae.Queries.GetCurriculumVitae
{
    public class GetCurriculumVitaeQueryHandler : IRequestHandler<GetCurriculumVitaeQuery, CurriculumVitaeListVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCurriculumVitaeQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CurriculumVitaeListVm> Handle(GetCurriculumVitaeQuery request, CancellationToken cancellationToken)
        {
            var curriculums = await _context.CurriculumsVitaes
                .Where(c => c.IDCOLABORADOR == request.IdColaborador)
                .OrderBy(c => c.IDCURRICULUMVITAE)
                .ProjectTo<CurriculumVitaeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new CurriculumVitaeListVm
            {
                CurriculumVitaes = curriculums,
                count = curriculums.Count
            };

            return vm;
        }
    }
}
