using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.BPMS.Queries.GetListaBPM
{
    public class BPMListVm
    {
        public IList<BPMDto> BPMs { get; set; }

        public int Count { get; set; }
    }
}
