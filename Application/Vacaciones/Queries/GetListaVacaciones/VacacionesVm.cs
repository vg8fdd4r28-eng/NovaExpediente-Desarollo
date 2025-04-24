using System.Collections.Generic;

namespace NovaExpediente.Application.Vacaciones.Queries.GetListaVacaciones
{
    public class VacacionesVm
    {
        public IList<VacacionDto> Vacaciones { get; set; }
        public int Count { get; set; }
    }
}
