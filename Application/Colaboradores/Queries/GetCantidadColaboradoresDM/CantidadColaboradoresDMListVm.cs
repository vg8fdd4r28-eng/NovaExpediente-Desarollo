using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Colaboradores.Queries.GetCantidadColaboradoresDM
{
    public class CantidadColaboradoresDMListVm
    {
        public IList<CantidadColaboradoresDMDto> Colaboradores { get; set; }

        public int Count { get; set; }
    }
}
