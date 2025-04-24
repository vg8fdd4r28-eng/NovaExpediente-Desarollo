using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ErpCores.Queries.GetListaErpCoreConsulta
{
    public class ErpCoreConsultaListVm
    {
        public IList<ErpCoreConsultaDto> ErpCores { get; set; }

        public int Count { get; set; }
    }
}
