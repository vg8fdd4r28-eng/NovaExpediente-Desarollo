using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.NivelesIdiomas.Queries.GetListaNivelIdiomaXdioma
{
    public class NivelIdiomaXdiomaListVm
    {
        public IList<NivelIdiomaXdiomaDto> NivelesIdioma { get; set; }

        public int Count { get; set; }
    }
}
