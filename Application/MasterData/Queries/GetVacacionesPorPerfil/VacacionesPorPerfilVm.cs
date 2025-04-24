using System.Collections.Generic;

namespace NovaExpediente.Application.MasterData.Queries.GetVacacionesPorPerfil
{
    public class VacacionesPorPerfilVm
    {
        public IList<VacacionPerfilDto> Vacaciones { get; set; }
        public int Count { get; set; }
    }
}
