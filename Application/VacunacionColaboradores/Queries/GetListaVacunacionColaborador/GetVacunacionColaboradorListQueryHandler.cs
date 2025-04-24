
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.VacunacionColaboradores.Queries.GetListaVacunacionColaborador
{
    public class GetVacunacionColaboradorListQueryHandler : IRequestHandler<GetVacunacionColaboradorListQuery, VacunacionColaboradorListVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetVacunacionColaboradorListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VacunacionColaboradorListVm> Handle(GetVacunacionColaboradorListQuery request, CancellationToken cancellationToken)
        {


            var _VacunacionColaboradores = await _context.VacunacionColaboradores
                //.OrderBy(p => p.Nombre)
                .ProjectTo<VacunacionColaboradorDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new VacunacionColaboradorListVm
            {
                VacunacionColaboradores = _VacunacionColaboradores,
                Count = _VacunacionColaboradores.Count
            };

            return vm;
        }
    }
}
