using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Tecnologias.Queries.GetListaTecnologiaConsulta
{
    public class TecnologiaConsultaVm
    {
        public IList<TecnologiaConsultaDto> Tecnologias { get; set; }

        public int Count { get; set; }
    }
}
