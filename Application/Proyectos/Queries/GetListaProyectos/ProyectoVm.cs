using System.Collections.Generic;

namespace NovaExpediente.Application.Proyectos.Queries.GetListaProyectos
{
    public class ProyectoVm
    {
        public IList<ProyectoDto> Proyectos { get; set; }

        public int Count { get; set; }
    }
}
