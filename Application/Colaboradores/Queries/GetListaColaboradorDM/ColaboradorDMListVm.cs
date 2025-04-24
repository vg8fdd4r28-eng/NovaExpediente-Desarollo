using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradorDM
{
    public class ColaboradorDMListVm
    {
        public IList<ColaboradorDMDto> ColaboradoresDM { get; set; }

        public int Count { get; set; }
    }
}
