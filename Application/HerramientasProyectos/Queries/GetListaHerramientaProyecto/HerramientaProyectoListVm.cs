using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.HerramientasProyectos.Queries.GetListaHerramientaProyecto
{
    public class HerramientaProyectoListVm
    {
        public IList<HerramientaProyectoDto> HerramientaProyectos { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
