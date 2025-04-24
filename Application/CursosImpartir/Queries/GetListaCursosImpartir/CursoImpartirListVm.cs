using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.CursosImpartir.Queries.GetListaCursosImpartir
{
    public class CursoImpartirListVm
    {
        public IList<CursoImpartirDto> CursosImpartir { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
