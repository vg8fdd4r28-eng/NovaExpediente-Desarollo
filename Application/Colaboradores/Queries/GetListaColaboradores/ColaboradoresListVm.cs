using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradores
{
    public class ColaboradoresListVm
    {
        public IList<ColaboradoresDto> Colaboradores { get; set; }

        public int Count { get; set; }
    }
}
