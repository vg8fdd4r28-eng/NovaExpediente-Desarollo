using System.Collections.Generic;

namespace NovaExpediente.Application.UnosAUnosEstados.Queries.GetListaUnosAUnosEstados
{
    public class ListaUnosAUnosEstadosVm
    {
        public IList<UnosAUnosEstadosDto> unosAUnosEstados { get; set; }
        public int count;
    }
}
