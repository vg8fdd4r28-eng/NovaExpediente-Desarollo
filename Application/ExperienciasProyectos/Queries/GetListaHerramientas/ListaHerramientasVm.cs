using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ExperienciasProyectos.Queries.GetListaHerramientas
{
    public class ListaHerramientasVm
    {
        public IList<HerramientaParaProyectoDto> Herramientas { get; set; }
        public IList<HerramientaParaProyectoDto> Tecnologias { get; set; }
        public int Count { get; set; }
    }
}
