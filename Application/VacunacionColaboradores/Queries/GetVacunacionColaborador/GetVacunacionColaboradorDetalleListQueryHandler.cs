
    
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.VacunacionColaboradores.Queries.GetVacunacionColaboradorDetalle
{
    public class GetVacunacionColaboradorDetalleListQueryHandler : IRequestHandler<GetVacunacionColaboradorDetalleListQuery, VacunacionColaboradorDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetVacunacionColaboradorDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VacunacionColaboradorDetalleVm> Handle(GetVacunacionColaboradorDetalleListQuery request, CancellationToken cancellationToken)
        {
            var _VacunacionColaborador = await _context.VacunacionColaboradores
                .Where(c => c.IdColaborador == request.IdColaborador)
                .Include(c => c.VacunasColaborador)
                .OrderBy(x => x.IdVacunacionColaborador)
                .ProjectTo<VacunacionColaboradorDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new VacunacionColaboradorDetalleVm
            {
                VacunacionColaborador = _VacunacionColaborador.FirstOrDefault()
            };

            return vm;
        }


    }
}
