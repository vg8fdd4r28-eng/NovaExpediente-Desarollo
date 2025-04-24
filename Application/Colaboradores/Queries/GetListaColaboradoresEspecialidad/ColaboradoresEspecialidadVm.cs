using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresEspecialidad
{
    public class ColaboradoresEspecialidadVm
    {
        public IList<ColaboradoresEspecialidadDto> Colaboradores { get; set; }

        public int CANTIDAD { get; set; }



    }
}

