using System.Collections.Generic;

namespace NovaExpediente.Application.Buses.Queries.GetListaBus
{
    public class BusVm
    {
        public IList<BusDto> Buses { get; set; }

        public int Count { get; set; }
    }
}
