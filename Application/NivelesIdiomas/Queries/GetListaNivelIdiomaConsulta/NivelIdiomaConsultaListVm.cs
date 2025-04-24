using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.NivelesIdiomas.Queries.GetListaNivelIdiomaConsulta
{
    public class NivelIdiomaConsultaListVm
    {
        public IList<NivelIdiomaConsultaDto> NivelIdioma { get; set; }

        public int Count { get; set; }
    }
}
