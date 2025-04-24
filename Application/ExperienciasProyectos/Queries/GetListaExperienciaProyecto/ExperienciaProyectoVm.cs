using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ExperienciasProyectos.Queries.GetListaExperienciaProyecto
{
    public class ExperienciaProyectoVm
    {
        public IList<ExperienciaProyectoDto> ExperienciaProyectos { get; set; }

        public int Count { get; set; }
    }
}
