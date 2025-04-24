using System.Collections.Generic;

namespace NovaExpediente.Application.Paises.Queries.GetListaPais
{
    public class PaisVm
    {
        public IList<PaisDto> Paises { get; set; }

        public int Count { get; set; }
    }
}
