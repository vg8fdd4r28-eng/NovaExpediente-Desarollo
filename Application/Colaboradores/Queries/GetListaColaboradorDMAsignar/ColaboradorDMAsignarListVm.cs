using System.Collections.Generic;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradorDMAsignar
{
    public class ColaboradorDMAsignarListVm
    {
        public IList<ColaboradorDMAsignarDto> ColaboradoresDM { get; set; }

        public int Count { get; set; }
    }
}
