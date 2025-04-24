using MediatR;
using System;
using System.Collections.Generic;

namespace NovaExpediente.Application.Permisos.Queries.GetPermisosPorPerfil
{
    public class GetPermisosPorPerfilQuery : IRequest<PermisoPorPerfilVm>
    {
        public int IdColaborador { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<string> Roles { get; set; }
    }
}
