using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresGenero
{
    public class ColaboradoresGeneroListVm
    {
        public IList<ColaboradoresGeneroDto> Colaboradores { get; set; }


        public int Cantidad { get; set; }


    }
}
