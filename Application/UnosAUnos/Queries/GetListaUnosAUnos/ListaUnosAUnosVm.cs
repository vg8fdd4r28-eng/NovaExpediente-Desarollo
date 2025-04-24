using System.Collections.Generic;

namespace NovaExpediente.Application.UnosAUnos.Queries.GetListaUnosAUnos
{
    public class ListaUnosAUnosVm
    {
        public IList<UnoAUnoDto> unosAUnos { get; set; }
        public int count;
    }
}
