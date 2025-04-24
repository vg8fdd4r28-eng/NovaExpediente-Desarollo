using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.LenguajesProgramacion.Queries.GetListaLenguajeProgramacion
{
    public class LenguajeProgramacionVm
    {
        public IList<LenguajeProgramacionDto> LenguajesProgramacion { get; set; }

        public int Count { get; set; }
    }
}
