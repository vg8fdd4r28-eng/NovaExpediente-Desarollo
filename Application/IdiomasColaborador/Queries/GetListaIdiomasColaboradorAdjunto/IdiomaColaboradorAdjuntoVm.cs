using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.IdiomasColaborador.Queries.GetListaIdiomasColaboradorAdjunto
{
    public class IdiomaColaboradorAdjuntoVm
    {
        public IList<IdiomaColaboradorAdjuntoDto> IdiomasColaborador { get; set; }

        public int Count { get; set; }
    }
}
