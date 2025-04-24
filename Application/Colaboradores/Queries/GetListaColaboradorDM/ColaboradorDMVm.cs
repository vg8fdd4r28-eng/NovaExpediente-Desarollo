using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradorDM
{
    public class ColaboradorDMVm
    {
        public IList<ColaboradorDMDto> ColaboradoresDM { get; set; }

        public int Count { get; set; }
    }
}
