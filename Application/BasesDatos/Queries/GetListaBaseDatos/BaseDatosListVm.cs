using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.BasesDatos.Queries.GetListaBaseDatos
{
    public class BaseDatosListVm
    {
        public IList<BaseDatosDto> BasesDatos { get; set; }

        public int Count { get; set; }
    }
}
