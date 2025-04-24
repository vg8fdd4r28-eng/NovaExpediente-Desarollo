using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.HerramientasCalidad.Queries.GetListaCalidad
{
    public class CalidadVm
    {
        public IList<CalidadDto> Calidads { get; set; }

        public int Count { get; set; }
    }
}
