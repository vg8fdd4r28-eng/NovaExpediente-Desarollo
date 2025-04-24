using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.AreasOrganizativas.Queries.GetListaAreaOrganizativa
{
    public class AreaOrganizativaListVm
    {
        public IList<AreaOrganizativaDto> areaOrganizativas { get; set; }

        public int Count { get; set; }
    }
}
