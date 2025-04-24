using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Idiomas.Queries.GetListaIdiomas
{
    public class IdiomaVm
    {
        public IList<IdiomaDto> Idioma { get; set; }

        public int Count { get; set; }
    }
}
