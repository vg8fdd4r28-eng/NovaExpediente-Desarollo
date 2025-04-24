
using System.Collections.Generic;

namespace NovaExpediente.Application.Vacunas.Queries.GetListaVacuna
{
    public class VacunaListVm
    {
        public IList<VacunaDto> Vacunas { get; set; }
        public int Count { get; set; }
    }
}