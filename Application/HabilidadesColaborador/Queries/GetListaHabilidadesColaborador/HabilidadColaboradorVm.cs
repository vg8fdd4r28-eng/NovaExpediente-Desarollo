using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.HabilidadesColaborador.Queries.GetListaHabilidadesColaborador
{
    public class HabilidadColaboradorVm
    {
        public IList<HabilidadColaboradorDto> HabilidadesColaborador { get; set; }

        public int Count { get; set; }
    }
}
