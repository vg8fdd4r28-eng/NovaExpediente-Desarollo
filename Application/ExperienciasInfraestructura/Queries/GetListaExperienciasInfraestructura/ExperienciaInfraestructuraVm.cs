using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ExperienciasInfraestructura.Queries.GetListaExperienciasInfraestructura
{
    public class ExperienciaInfraestructuraVm
    {
        public IList<ExperienciaInfraestructuraDto> ExperienciasInfraestructuras { get; set; }

        public int Count { get; set; }
    }
}
