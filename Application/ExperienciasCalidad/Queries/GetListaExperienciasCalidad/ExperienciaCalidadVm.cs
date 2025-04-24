using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ExperienciasCalidad.Queries.GetListaExperienciasCalidad
{
    public class ExperienciaCalidadVm
    {
        public IList<ExperienciaCalidadDto> ExperienciasCalidad { get; set; }

        public int Count { get; set; }
    }
}
