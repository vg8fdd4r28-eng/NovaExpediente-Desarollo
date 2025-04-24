using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.AmbientesDesarrollo.Queries.GetListaAmbienteDesarrolloConsulta
{
    public class AmbienteDesarrolloConsultaListVm
    {
        public IList<AmbienteDesarrolloConsultaDto> AmbientesDesarrollo { get; set; }

        public int Count { get; set; }
    }
}
