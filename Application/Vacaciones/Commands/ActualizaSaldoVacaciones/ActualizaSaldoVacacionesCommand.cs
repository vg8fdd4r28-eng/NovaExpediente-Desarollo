using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using NovaExpediente.Application.Colaboradores.Queries.GetColaboradorVacacionesPorPerfil;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Vacaciones.Commands.ActualizaSaldoVacaciones
{
    public class ActualizaSaldoVacacionesCommand : IRequest<string>
    {

        public class UpsertCategoryCommandHandler : IRequestHandler<ActualizaSaldoVacacionesCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;
            private readonly INominaService _service;
            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context, INominaService service)
            {
                _context = context;
                _service = service;
            }

            public async Task<string> Handle(ActualizaSaldoVacacionesCommand request, CancellationToken cancellationToken)
            {
                // se obtiene un diccionario donde la llave es el identificador del colaborador en 
                // el expediente digital y el valor asociado es el identificador en nómina
                Dictionary<int, int?> identificadoresNomina= new Dictionary<int, int?>();

                List<ColaboradorVacacionesPorPerfilDto> colaboradores = await _context.GetColaboradoresNomina();
                foreach (ColaboradorVacacionesPorPerfilDto item in colaboradores)
                {
                    identificadoresNomina.Add(item.IDCOLABORADOR, item.IDNOMINA);
                }

                // se obtiene un diccionario con el saldo de cada colaborador
                var saldos = await _service.ConsultaSaldo(identificadoresNomina);
                
                // se actualiza el saldo de vacaciones para cada colaborador en nómina
                await _context.ActualizaSaldoVacacionesNomina(saldos);

                // se actualizan los saldos para los colaboradores que no está en nómina
                await _context.ActualizaSaldoVacacionesServiciosProfesionales();

                return "";

            }
        }


        public class ColaboradorSaldo {
            public decimal diasDisponibles { get; set; }

        }

    }
}
