using AutoMapper;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Application.MasterData.Queries.GetListaSolicitudes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetAntiguedadSolicitudes
{
    public class GetAntiguedadSolicitudesQueryHandler : IRequestHandler<GetAntiguedadSolicitudesQuery, CantidadPorAntiguedadVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetAntiguedadSolicitudesQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CantidadPorAntiguedadVm> Handle(GetAntiguedadSolicitudesQuery request, CancellationToken cancellationToken)
        {
            GetListaSolicitudesBuzonQuery buzonSeguimiento = new GetListaSolicitudesBuzonQuery() { IdUsuario = 0, Tipo = "Tracking"};
            GetListaSolicitudesBuzonQueryHandler solicitudes = new GetListaSolicitudesBuzonQueryHandler(_context, _mapper);
            var resultado = await solicitudes.Handle(buzonSeguimiento, cancellationToken);

            var clasificacion = resultado.SolicitudesMasterData
                                         .GroupBy(c => c.Rango)
                                         .OrderBy(c => c.Key)
                                         .Select(g => new AntiguedadDto
                                         {
                                             Tipo = (g.Key == RangoAntiguedad.Verde ? "A tiempo"
                                                                                : (g.Key == RangoAntiguedad.Amarillo ? "Requiere atención"
                                                                                                                     : "Vencidas")),
                                             Cantidad = g.Count(),
                                             Color = (g.Key == RangoAntiguedad.Verde ? "#2eb85c"
                                                                                : (g.Key == RangoAntiguedad.Amarillo ? "#F9B115"
                                                                                             : "#FF0000")),
                                         })
                                         .ToList();
             var vm = new CantidadPorAntiguedadVm
             {
               Tipos = clasificacion
             };

            return vm;
        }
    }
}
