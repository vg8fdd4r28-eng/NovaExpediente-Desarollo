using MediatR;
using System;
using System.Collections.Generic;

namespace NovaExpediente.Application.MasterData.Queries.GetVacacionesPorPerfil
{
    public class GetVacacionesPorPerfilQuery : IRequest<VacacionesPorPerfilVm>
    {
        public int IdColaborador { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<string> Roles { get; set; }
    }
}
