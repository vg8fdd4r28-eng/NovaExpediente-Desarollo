using System.Collections.Generic;

namespace NovaExpediente.Application.Permisos.Queries.GetPermisosPorPerfil  
{
    public class PermisoPorPerfilVm
    {
        public IList<PermisoPerfilDto> Permisos { get; set; }
        public int Count { get; set; }
    }
}
