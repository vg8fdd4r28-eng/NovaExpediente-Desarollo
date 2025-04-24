using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ProyectoAmbientes.Queries.GetListaProyectoAmbientes
{
    public class ProyectoAmbienteListVm
    {
        public IList<ProyectoAmbienteDto> ProyectoAmbientes { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
