using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Especialidades.Queries.GetListaEspecialidad
{
    public class EspecialidadVm
    {
        public IList<EspecialidadDto> Especialidades { get; set; }

        public int Count { get; set; }
    }
}
