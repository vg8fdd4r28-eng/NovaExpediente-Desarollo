using System.Collections.Generic;

namespace NovaExpediente.Application.Cantones.Queries.GetListaCanton
{
    public class CantonVm
    {
        public IList<CantonDto> Cantones { get; set; }

        public int Count { get; set; }
    }
}
