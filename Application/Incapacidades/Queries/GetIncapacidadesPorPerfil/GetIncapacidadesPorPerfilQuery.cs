using MediatR;
using System;
using System.Collections.Generic;

namespace NovaExpediente.Application.Incapacidades.Queries.GetIncapacidadesPorPerfil
{
    public class GetIncapacidadesPorPerfilQuery : IRequest<IncapacidadesPorPerfilVm>
    {
        public int IdColaborador { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<string> Roles { get; set; }
    }
}
