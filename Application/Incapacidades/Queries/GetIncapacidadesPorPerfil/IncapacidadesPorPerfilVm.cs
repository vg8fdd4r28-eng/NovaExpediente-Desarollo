using System.Collections.Generic;

namespace NovaExpediente.Application.Incapacidades.Queries.GetIncapacidadesPorPerfil
{
    public class IncapacidadesPorPerfilVm
    {
        public IList<IncapacidadPerfilDto> Incapacidades { get; set; }
        public int Count { get; set; }
    }
}
