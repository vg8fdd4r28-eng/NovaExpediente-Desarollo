using System.Collections.Generic;

namespace NovaExpediente.Application.AplicacionMovil.Eventos.GetListaEventos
{
    public class ListaEventosVm
    {
        public IList<EventoDto> Eventos { get; set; }
        public int Count;
    }
}
