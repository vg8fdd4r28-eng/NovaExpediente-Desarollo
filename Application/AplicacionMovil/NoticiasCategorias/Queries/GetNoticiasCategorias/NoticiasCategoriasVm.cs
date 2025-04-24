using System.Collections.Generic;

namespace NovaExpediente.Application.AplicacionMovil.NoticiasCategorias.Queries.GetNoticiasCategorias
{
    public class NoticiasCategoriasVm
    {
        public IList<NoticiasCategoriasDto> NoticiasCategorias { get; set; }
        public int Count { get; set; }
    }
}
