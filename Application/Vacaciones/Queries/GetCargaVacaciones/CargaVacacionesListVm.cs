using System.Collections.Generic;

namespace NovaExpediente.Application.Vacaciones.Queries.GetCargaVacaciones
{
    public class CargaVacacionesListVm
    {
        public IList<CargaVacacionesDto> Cargas { get; set; }

        public int count;
    }
}
