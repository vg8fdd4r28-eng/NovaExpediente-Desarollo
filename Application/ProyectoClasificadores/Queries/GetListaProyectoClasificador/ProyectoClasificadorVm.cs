using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ProyectoClasificadores.Queries.GetListaProyectoClasificador
{
    public class ProyectoClasificadorVm
    {
        public IList<ProyectoClasificadorDto> ProyectoClasificador { get; set; }

        public int Count { get; set; }
    }
}
