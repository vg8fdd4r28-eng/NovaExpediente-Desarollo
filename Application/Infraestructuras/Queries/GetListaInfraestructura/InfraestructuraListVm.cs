using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Infraestructuras.Queries.GetListaInfraestructura
{
    public class InfraestructuraListVm
    {
        public IList<InfraestructuraDto> Infraestructuras { get; set; }

        public int Count { get; set; }
    }
}
