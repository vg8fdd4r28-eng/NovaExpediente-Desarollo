using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.HerramientasCalidad.Queries.GetListaCalidadConsulta
{
    public class CalidadConsultaVm
    {
        public IList<CalidadConsultaDto> Calidads { get; set; }

        public int Count { get; set; }
    }
}
