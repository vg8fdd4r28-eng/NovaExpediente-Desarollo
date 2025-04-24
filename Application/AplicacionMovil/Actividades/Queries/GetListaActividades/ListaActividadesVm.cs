using System.Collections.Generic;

namespace NovaExpediente.Application.AplicacionMovil.Actividades.Queries.GetListaActividades
{
    public class ListaActividadesVm
    {
        public IList<ActividadDto> Actividades { get; set; }
        public int count;
    }
}
