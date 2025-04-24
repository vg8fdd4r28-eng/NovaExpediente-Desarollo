using AutoMapper;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Application.MasterData.Queries.GetAntiguedadSolicitudes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetAntiguedadExpediente
{
    public class GetAntiguedadExpedienteQueryHandler : IRequestHandler<GetAntiguedadExpedienteQuery, CantidadPorAntiguedadVm>
    {

        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetAntiguedadExpedienteQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CantidadPorAntiguedadVm> Handle(GetAntiguedadExpedienteQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, decimal> antiguedad = await _context.ActualizacionExpedienteTotal();
            List<AntiguedadDto> lista = new List<AntiguedadDto>();

            foreach (var key in antiguedad.Keys)
            {
                lista.Add(new AntiguedadDto()
                {
                    Tipo = (key.Substring(2, key.Length - 2) == "Verde" ? "<= 30 días"
                                                                        : (key.Substring(2, key.Length - 2) == "Amarillo" ? ">30 <= 60 días"
                                                                                                                          : "> 60 días")),
                    Cantidad = Math.Round(antiguedad[key], 2),
                    Color = (key.Substring(2, key.Length - 2) == "Verde" ? "#2eb85c"
                                                                         : (key.Substring(2, key.Length - 2) == "Amarillo" ? "#F9B115"
                                                                                                                           : "#FF0000"))
                });
            }

            var vm = new CantidadPorAntiguedadVm
            {
                Tipos = lista
            };

            return vm;
        }
    }
}
