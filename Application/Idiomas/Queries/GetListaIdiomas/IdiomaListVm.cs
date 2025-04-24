using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Idiomas.Queries.GetListaIdiomas
{
    public class IdiomaListVm
    {
        public IList<IdiomaDto> Idiomas { get; set; }

        public int count;
    }
}
