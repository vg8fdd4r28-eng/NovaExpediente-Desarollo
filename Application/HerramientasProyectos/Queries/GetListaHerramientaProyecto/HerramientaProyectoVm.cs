using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.HerramientasProyectos.Queries.GetListaHerramientaProyecto
{
    public class HerramientaProyectoVm
    {
        public IList<HerramientaProyectoDto> HerramientaProyectos { get; set; }

        public int Count { get; set; }
    }
}
