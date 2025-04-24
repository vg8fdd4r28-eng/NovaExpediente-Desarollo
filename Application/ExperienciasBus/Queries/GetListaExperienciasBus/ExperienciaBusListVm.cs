using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ExperienciasBus.Queries.GetListaExperienciasBus
{
    public class ExperienciaBusListVm
    {
        public IList<ExperienciaBusDto> ExperienciasBus { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
