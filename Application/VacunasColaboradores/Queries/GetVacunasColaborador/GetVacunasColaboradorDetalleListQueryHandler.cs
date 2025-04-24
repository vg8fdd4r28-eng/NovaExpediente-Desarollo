
    
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.VacunasColaboradores.Queries.GetVacunasColaboradorDetalle
{
    public class GetVacunasColaboradorDetalleListQueryHandler : IRequestHandler<GetVacunasColaboradorDetalleListQuery, VacunasColaboradorDetalleVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetVacunasColaboradorDetalleListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VacunasColaboradorDetalleVm> Handle(GetVacunasColaboradorDetalleListQuery request, CancellationToken cancellationToken)
        {
            var _VacunasColaborador = await _context.VacunasColaboradores
                .Where(c => c.IdVacunasColaborador == request.IdVacunasColaborador)
                .OrderBy(x => x.IdVacunasColaborador)
                .ProjectTo<VacunasColaboradorDetalleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new VacunasColaboradorDetalleVm
            {
                VacunasColaborador = _VacunasColaborador.FirstOrDefault()
            };

            return vm;
        }


    }
}
