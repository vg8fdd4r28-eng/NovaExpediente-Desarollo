using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Buses.Queries.GetListaBusConsulta
{
    public class BusConsultaVm
    {
        public IList<BusConsultaDto> Buses { get; set; }

        public int Count { get; set; }
    }
}
