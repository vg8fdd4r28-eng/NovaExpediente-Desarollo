using System.Collections.Generic;

namespace NovaExpediente.Application.Clouds.Queries.GetListaCloud
{
    public class CloudListVm
    {
        public IList<CloudDto> Clouds{ get; set; }

        public int Count { get; set; }
    }
}
