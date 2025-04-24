using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.NivelesIdiomas.Queries.GetListaNivelIdioma
{
    public class NivelIdiomaVm
    {
        public IList<NivelIdiomaDto> NivelIdiomas { get; set; }

        public int Count { get; set; }
    }
}
