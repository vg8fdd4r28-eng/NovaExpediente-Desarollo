using MediatR;
using System;

namespace NovaExpediente.Application.Vacaciones.Queries.CalculaCantidadDias
{
    public class GetCalculaCantidadDiasListQuery : IRequest<CalculaCantidadDiasVm>
    {
        public int IdColaborador { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

    }
}
