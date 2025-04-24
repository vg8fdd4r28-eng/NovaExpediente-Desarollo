using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.LenguajesProgramacion.Queries.GetListaLenguajeProgramacionConsulta
{
    public class LenguajeProgramacionConsultaListVm
    {
        public IList<LenguajeProgramacionConsultaDto> LenguajesProgramacion { get; set; }

        public int Count { get; set; }
    }
}
