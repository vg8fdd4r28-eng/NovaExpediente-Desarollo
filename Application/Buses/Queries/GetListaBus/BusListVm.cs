using System.Collections.Generic;

namespace NovaExpediente.Application.Buses.Queries.GetListaBus
{
    public class BusListVm
    {
        public IList<BusDto> Buses { get; set; }

        public int Count { get; set; }
    }
}
