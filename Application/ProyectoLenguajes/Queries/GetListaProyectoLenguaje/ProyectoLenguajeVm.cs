using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ProyectoLenguajes.Queries.GetListaProyectoLenguaje
{
    public class ProyectoLenguajeVm
    {
        public IList<ProyectoLenguajeDto> ProyectoLenguaje { get; set; }

        public int Count { get; set; }
    }
}
