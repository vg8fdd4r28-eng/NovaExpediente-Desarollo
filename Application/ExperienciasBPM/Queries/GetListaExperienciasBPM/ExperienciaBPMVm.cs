using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ExperienciasBPM.Queries.GetListaExperienciasBPM
{
    public class ExperienciaBPMVm
    {
        public IList<ExperienciaBPMDto> ExperienciasBPM { get; set; }

        public int Count { get; set; }
    }
}
