using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.CursosColaborador.Queries.GetListaCursosColaboradorAdjunto
{
    public class CursoColaboradorAdjuntoListVm
    {
        public IList<CursoColaboradorAdjuntoDto> CursosColaborador { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
