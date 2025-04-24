using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.TiposCore.Queries.GetListaTiposCore
{
    public class TipoCoreListVm
    {
        public IList<TiposCoreDto> TiposCores { get; set; }
        public int Count { get; set; }
    }
}
