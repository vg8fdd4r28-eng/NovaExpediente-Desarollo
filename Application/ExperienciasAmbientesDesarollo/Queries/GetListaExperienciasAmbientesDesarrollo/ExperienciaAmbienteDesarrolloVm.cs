using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ExperienciasAmbientesDesarollo.Queries.GetListaExperienciasAmbientesDesarrollo 
{
    public class ExperienciaAmbienteDesarrolloVm
    {
        public IList<ExperienciaAmbienteDesarrolloDto> ExperienciaAmbientesDesarrollo { get; set; }

        public int Count { get; set; }
    }
}
