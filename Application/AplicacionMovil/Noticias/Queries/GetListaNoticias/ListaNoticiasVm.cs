using System.Collections.Generic;

namespace NovaExpediente.Application.AplicacionMovil.Noticias.Queries.GetListaNoticias
{
    public class ListaNoticiasVm
    {
        public IList<NoticiaDto> Noticias { get; set; }
        public int count;
    }
}
