
using System.Collections.Generic;

namespace NovaExpediente.Application.VacunacionColaboradores.Queries.GetListaVacunacionColaborador
{
    public class VacunacionColaboradorListVm
    {
        public IList<VacunacionColaboradorDto> VacunacionColaboradores { get; set; }
        public int Count { get; set; }
    }
}