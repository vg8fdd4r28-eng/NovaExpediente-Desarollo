using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.TecnologiasProyectos.Queries.GetListaTecnologiaProyecto
{
    public class TecnologiaProyectoListVm
    {
        public IList<TecnologiaProyectoDto> TecnologiaProyectos { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
