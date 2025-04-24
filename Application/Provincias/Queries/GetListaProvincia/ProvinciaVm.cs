using System.Collections.Generic;

namespace NovaExpediente.Application.Provincias.Queries.GetListaProvincia
{
    public class ProvinciaVm
    {
        public IList<ProvinciaDto> Provincias { get; set; }

        public int Count { get; set; }
    }
}
