using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Cursos.Queries.GetListaCursos
{
    public class CursosListVm
    {
        public IList<CursosDto> Cursos { get; set; }

        public int Count { get; set; }
    }
}
