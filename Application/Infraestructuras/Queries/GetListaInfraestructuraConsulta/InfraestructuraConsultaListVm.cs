using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Infraestructuras.Queries.GetListaInfraestructuraConsulta
{
    public class InfraestructuraConsultaListVm
    {
        public IList<InfraestructuraConsultaDto> Infraestructuras { get; set; }

        public int Count { get; set; }
    }
}
