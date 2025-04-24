using System.Collections.Generic;

namespace NovaExpediente.Application.Cantones.Queries.GetListaCanton
{
    public class CantonListVm
    {
        public IList<CantonDto> Cantones { get; set; }

        public int Count { get; set; }
    }
}
