using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.CursosColaborador.Queries.GetListaCursosColaboradorAdjunto
{ 
    public class CursoColaboradorAdjuntoVm
    {
        public IList<CursoColaboradorAdjuntoDto> CursosColaborador { get; set; }

        public int Count { get; set; }
    }
}
