using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ExperienciasTecnologias.Queries.GetListaExperienciasTecnologias
{
    public class ExperienciaTecnologiaVm
    {
        public IList<ExperienciaTecnologiaDto> ExperienciaTecnologias { get; set; }

        public int Count { get; set; }
    }
}
