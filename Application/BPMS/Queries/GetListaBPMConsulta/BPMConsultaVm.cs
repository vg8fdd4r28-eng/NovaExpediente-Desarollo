using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.BPMS.Queries.GetListaBPMConsulta
{
    public class BPMConsultaVm
    {
        public IList<BPMConsultaDto> BPMS { get; set; }

        public int Count { get; set; }
    }
}
