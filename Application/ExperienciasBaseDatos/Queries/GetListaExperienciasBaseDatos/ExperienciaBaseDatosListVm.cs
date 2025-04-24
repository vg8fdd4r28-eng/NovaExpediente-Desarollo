using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ExperienciasBaseDatos.Queries.GetListaExperienciasBaseDatos
{
    public class ExperienciaBaseDatosListVm
    {
        public IList<ExperienciaBaseDatosDto> ExperienciasBaseDatos { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
