using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ExperienciasBus.Queries.GetListaExperienciasBus
{
    public class ExperienciaBusVm
    {
        public IList<ExperienciaBusDto> ExperienciasBuss { get; set; }

        public int Count { get; set; }
    }
}
