using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradores
{
    public class ColaboradoresVm
    {
        public IList<ColaboradoresDto> Colaboradores { get; set; }

        public int Count { get; set; }
    }
}
