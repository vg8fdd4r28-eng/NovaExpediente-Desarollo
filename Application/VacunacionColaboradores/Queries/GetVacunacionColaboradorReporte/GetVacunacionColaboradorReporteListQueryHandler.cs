using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.VacunacionColaboradores.Queries.GetVacunacionColaboradorReporte
{
    public class GetVacunacionColaboradorReporteListQueryHandler : IRequestHandler<GetVacunacionColaboradorReporteListQuery, VacunacionColaboradorReporteVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;




        public GetVacunacionColaboradorReporteListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VacunacionColaboradorReporteVm> Handle(GetVacunacionColaboradorReporteListQuery request, CancellationToken cancellationToken)
        {
            VacunacionColaboradorReporteVm vm = new VacunacionColaboradorReporteVm();
            List<VacunacionColaboradorReporteDto> colaboradores = new List<VacunacionColaboradorReporteDto>();

            colaboradores = await _context.Colaboradores
                                    .Where(v => v.ACTIVO)
                                    .Include(v => v.DeliveryManager)
                                    .Include(v => v.Cliente)
                                    .Include(v => v.VacunacionColaborador)
                                    .ProjectTo<VacunacionColaboradorReporteDto>(_mapper.ConfigurationProvider)
                                    .OrderBy(v => v.NombreCompleto)
                                    .ToListAsync(cancellationToken);

            vm.VacunacionColaboradores = colaboradores;
            vm.Count = colaboradores.Count();

            return vm;
        }


    }
}
