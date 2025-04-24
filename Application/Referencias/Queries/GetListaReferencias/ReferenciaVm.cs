using System.Collections.Generic;

namespace NovaExpediente.Application.Referencias.Queries.GetListaReferencias
{
    public class ReferenciaVm
    {
        public IList<ReferenciaDto> Referencias { get; set; }

        public int Count { get; set; }
    }
}
