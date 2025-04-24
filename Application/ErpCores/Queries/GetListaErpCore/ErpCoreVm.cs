using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ErpCores.Queries.GetListaErpCore
{
    public class ErpCoreVm
    {
        public IList<ErpCoreDto> ErpCores { get; set; }

        public int Count { get; set; }
    }
}
