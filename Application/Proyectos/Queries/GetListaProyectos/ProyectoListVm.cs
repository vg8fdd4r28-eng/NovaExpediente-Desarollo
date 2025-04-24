using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Proyectos.Queries.GetListaProyectos
{
    public class ProyectoListVm
    {
        public IList<ProyectoDto> Proyectos { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
