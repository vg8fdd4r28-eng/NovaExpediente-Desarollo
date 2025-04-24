using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Puestos.Queries.GetListaPuesto
{
    public class PuestoListVm
    {
        public IList<PuestoDto> Puesto { get; set; }
        public int Count { get; set; }
    }
}
