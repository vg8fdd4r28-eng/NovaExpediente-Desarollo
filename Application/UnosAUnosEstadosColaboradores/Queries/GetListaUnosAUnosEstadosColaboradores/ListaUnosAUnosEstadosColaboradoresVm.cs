using System.Collections.Generic;

namespace NovaExpediente.Application.UnoAUnoEstadosColaboradores.Queries.GetListaUnoAUnoEstadosColaboradores
{
    public class ListaUnosAUnosEstadosColaboradoresVm
    {
        public IList<UnosAUnosEstadosColaboradoresDto> unosAUnosEstadosColaboradores { get; set; }
        public int count;
    }
}
