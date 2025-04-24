using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ExperienciasBPM.Queries.GetListaExperienciasBPM
{
    public class ExperienciaBPMListVm
    {
        public IList<ExperienciaBPMDto> ExperienciasBPM { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
