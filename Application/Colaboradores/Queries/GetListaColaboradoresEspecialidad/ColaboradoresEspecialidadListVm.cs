using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresEspecialidad
{
   public class ColaboradoresEspecialidadListVm
    {
        public IList<ColaboradoresEspecialidadDto> Colaboradores { get; set; }

        public int Cantidad { get; set; }
    }
}
