using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.HerramientasCalidad.Queries.GetListaCalidadConsulta
{
    public class CalidadConsultaListVm
    {
        public IList<CalidadConsultaDto> Calidads { get; set; }

        public int Count { get; set; }
    }
}
