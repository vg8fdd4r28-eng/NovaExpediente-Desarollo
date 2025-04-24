using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ExperienciasBaseDatos.Queries.GetListaExperienciasBaseDatos
{
    public class ExperienciaBaseDatosVm
    {
        public IList<ExperienciaBaseDatosDto> ExperienciasBaseDatos { get; set; }

        public int Count { get; set; }
    }
}
