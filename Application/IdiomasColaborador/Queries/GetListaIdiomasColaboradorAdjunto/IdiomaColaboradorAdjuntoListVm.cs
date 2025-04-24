using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.IdiomasColaborador.Queries.GetListaIdiomasColaboradorAdjunto
{
    public class IdiomaColaboradorAdjuntoListVm
    {
        public IList<IdiomaColaboradorAdjuntoDto> IdiomasColaborador { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
