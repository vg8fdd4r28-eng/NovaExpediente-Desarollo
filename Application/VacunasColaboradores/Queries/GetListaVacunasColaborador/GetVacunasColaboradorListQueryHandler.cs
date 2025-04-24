
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.VacunasColaboradores.Queries.GetListaVacunasColaborador
{
    public class GetVacunasColaboradorListQueryHandler : IRequestHandler<GetVacunasColaboradorListQuery, VacunasColaboradorListVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetVacunasColaboradorListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VacunasColaboradorListVm> Handle(GetVacunasColaboradorListQuery request, CancellationToken cancellationToken)
        {
            int idVacunacionColaborador = _context.VacunacionColaboradores.Where(v => v.IdColaborador == request.IdColaborador).FirstOrDefault().IdVacunacionColaborador;

            var _VacunasColaboradores = await _context.VacunasColaboradores
                .Where(v => v.IdVacunacionColaborador == idVacunacionColaborador)
                .OrderBy(v => v.Fecha)
                .ProjectTo<VacunasColaboradorDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new VacunasColaboradorListVm
            {
                VacunasColaborador = _VacunasColaboradores,
                Count = _VacunasColaboradores.Count
            };

            return vm;
        }
    }
}
