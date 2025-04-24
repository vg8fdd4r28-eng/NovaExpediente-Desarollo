using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.HerramientasCalidad.Queries.GetListaCalidad
{
    public class CalidadListVm
    {
        public IList<CalidadDto> Calidads { get; set; }

        public int Count { get; set; }
    }
}
