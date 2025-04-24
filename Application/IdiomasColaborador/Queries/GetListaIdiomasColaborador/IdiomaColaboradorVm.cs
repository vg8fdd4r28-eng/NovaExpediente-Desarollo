using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.IdiomasColaborador.Queries.GetListaIdiomasColaborador
{
    public class IdiomaColaboradorVm
    {
        public IList<IdiomaColaboradorDto> IdiomasColaborador { get; set; }

        public int Count { get; set; }
    }
}
