using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ProyectoAmbientes.Queries.GetListaProyectoAmbientes
{
    public class ProyectoAmbienteVm
    {
        public IList<ProyectoAmbienteDto> ProyectoAmbientes { get; set; }

        public int Count { get; set; }
    }
}
