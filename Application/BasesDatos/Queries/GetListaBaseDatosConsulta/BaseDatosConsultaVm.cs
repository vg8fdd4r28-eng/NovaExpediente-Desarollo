using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.BasesDatos.Queries.GetListaBaseDatosConsulta
{
    public class BaseDatosConsultaVm
    {
        public IList<BaseDatosConsultaDto> BasesDatos { get; set; }

        public int Count { get; set; }
    }
}
