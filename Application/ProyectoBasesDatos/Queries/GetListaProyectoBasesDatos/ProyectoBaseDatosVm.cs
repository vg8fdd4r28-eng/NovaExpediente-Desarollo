using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ProyectoBasesDatos.Queries.GetListaProyectoBaseDatos
{
    public class ProyectoBaseDatosVm
    {
        public IList<ProyectoBaseDatosDto> ProyectoBaseDatos { get; set; }

        public int Count { get; set; }
    }
}
