using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ColaboradorProyectos.Queries.GetListaColaboradorProyectos
{
    public class ColaboradorProyectoListVm
    {
        public IList<ColaboradorProyectoDto> ColaboradorProyectos { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
