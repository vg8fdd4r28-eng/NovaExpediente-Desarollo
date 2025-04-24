using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ProyectoTecnologias.Queries.GetListaProyectoTecnologia
{
    public class ProyectoTecnologiaListVm
    {
        public IList<ProyectoTecnologiaDto> ProyectoTecnologia { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
