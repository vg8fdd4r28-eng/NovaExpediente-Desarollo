using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ProyectoClasificadores.Queries.GetListaProyectoClasificador
{
    public class ProyectoClasificadorListVm
    {
        public IList<ProyectoClasificadorDto> ProyectoClasificador { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
