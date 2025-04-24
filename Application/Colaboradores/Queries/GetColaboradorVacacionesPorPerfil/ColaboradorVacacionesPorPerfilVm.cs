using System.Collections.Generic;

namespace NovaExpediente.Application.Colaboradores.Queries.GetColaboradorVacacionesPorPerfil
{
    public class ColaboradorVacacionesPorPerfilVm
    {
        public List<ColaboradorVacacionesPorPerfilDto> ColaboradorVacaciones { get; set; }
        public int Count { get; set; }
    }
}
