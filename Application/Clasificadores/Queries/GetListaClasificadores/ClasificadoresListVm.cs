using System.Collections.Generic;

namespace NovaExpediente.Application.Clasificadores.Queries.GetListaClasificadores
{
    public class ClasificadoresListVm
    {
        public IList<ClasificadoresDto> Clasificadores { get; set; }

        public int Count { get; set; }
    }
}
