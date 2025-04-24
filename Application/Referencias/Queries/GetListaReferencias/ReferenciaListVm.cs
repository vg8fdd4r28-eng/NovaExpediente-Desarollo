using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Referencias.Queries.GetListaReferencias
{
    public class ReferenciaListVm
    {
        public IList<ReferenciaDto> Referencias { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
