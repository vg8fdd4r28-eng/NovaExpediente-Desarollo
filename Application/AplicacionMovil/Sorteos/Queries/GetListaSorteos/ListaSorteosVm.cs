using System.Collections.Generic;

namespace NovaExpediente.Application.AplicacionMovil.Sorteos.Queries.GetListaSorteos
{
    public class ListaSorteosVm
    {
        public IList<SorteoDto> Sorteos { get; set; }
        public int Count;
    }
}
