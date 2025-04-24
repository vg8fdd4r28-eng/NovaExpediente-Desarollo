using System.Collections.Generic;

namespace NovaExpediente.Application.MasterData.Queries.GetVacacionesAPP
{
    public class VacacionesAPPVm
    {
        public IList<VacacionAPPDto> Vacaciones { get; set; }
        public int Count { get; set; }
    }
}
