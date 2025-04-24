using System.Collections.Generic;

namespace NovaExpediente.Application.AplicacionMovil.Convenios.Queries.GetListaConvenios
{
    public class ListaConveniosVm
    {
        public IList<ConvenioDto> Convenios { get; set; }
        public int count;
    }
}
