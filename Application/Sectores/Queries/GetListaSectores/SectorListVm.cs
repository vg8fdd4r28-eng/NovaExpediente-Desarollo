using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Sectores.Queries.GetListaSectores
{
    public  class SectorListVm
    {
        public IList<SectorDto> Sectores { get; set; }

        public int Count { get; set; }
    }
}
