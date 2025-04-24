using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Buses.Queries.GetListaBusConsulta
{
    public class BusConsultaListVm
    {
        public IList<BusConsultaDto> Buses { get; set; }

        public int Count { get; set; }
    }
}
