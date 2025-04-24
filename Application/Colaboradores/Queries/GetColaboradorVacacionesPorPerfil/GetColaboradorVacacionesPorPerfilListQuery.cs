using MediatR;
using System.Collections.Generic;

namespace NovaExpediente.Application.Colaboradores.Queries.GetColaboradorVacacionesPorPerfil
{
   public class GetColaboradorVacacionesPorPerfilListQuery : IRequest<ColaboradorVacacionesPorPerfilVm>
    {

        public int IdColaborador { get; set; }
        public List<string> Roles { get; set; }
    }
}





