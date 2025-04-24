using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.AmbientesDesarrollo.Queries.GetListaAmbienteDesarrollo
{
    public class AmbienteDesarrolloVm
    {
        public IList<AmbienteDesarrolloDto> AmbientesDesarrollo { get; set; }

        public int Count { get; set; }
    }
}
