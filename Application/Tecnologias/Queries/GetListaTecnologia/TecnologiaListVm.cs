using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Tecnologias.Queries.GetListaTecnologia
{
    public class TecnologiaListVm
    {
        public IList<TecnologiaDto> Tecnologias { get; set; }

        public int Count { get; set; }
    }
}
