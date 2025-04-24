using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Especialidades.Queries.GetListaEspecialidadConsulta
{
    public class EspecialidadConsultaListVm
    {
        public IList<EspecialidadConsultaDto> Especialidades { get; set; }

        public int Count { get; set; }
    }
}
