using System.Collections.Generic;

namespace NovaExpediente.Application.Colaboradores.Queries.GetColaboradorVacacionesConsulta
{
    public class ColaboradorVacacionesConsultaVm
    {
        public List<ColaboradorVacacionesConsultaDto> ColaboradorVacaciones { get; set; }
        public int Count { get; set; }
    }
}
