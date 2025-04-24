using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.NivelesIdiomas.Queries.GetListaNivelIdiomaConsulta
{
    public class NivelIdiomaConsultaVm
    {
        public IList<NivelIdiomaConsultaDto> NivelIdiomas { get; set; }

        public int Count { get; set; }
    }
}
