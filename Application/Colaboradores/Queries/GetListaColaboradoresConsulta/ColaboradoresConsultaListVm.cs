using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresConsulta
{
    public class ColaboradoresConsultaListVm
    {
        public IList<ColaboradoresConsultaDto> Colaboradores { get; set; }


        public int Cantidad { get; set; }


    }
}
