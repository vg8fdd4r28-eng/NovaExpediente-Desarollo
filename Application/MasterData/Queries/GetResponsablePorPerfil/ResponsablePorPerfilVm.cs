using System.Collections.Generic;

namespace NovaExpediente.Application.MasterData.Queries.GetResponsablePorPerfil
{
    public class ResponsablePorPerfilVm
    {
        public IList<ResponsablePorPerfilDto> Responsables { get; set; }
        public int Count { get; set; }
    }
}
