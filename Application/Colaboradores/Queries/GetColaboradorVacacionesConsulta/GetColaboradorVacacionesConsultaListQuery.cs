using MediatR;
using System.Collections.Generic;

namespace NovaExpediente.Application.Colaboradores.Queries.GetColaboradorVacacionesConsulta
{
   public class GetColaboradorVacacionesConsultaListQuery : IRequest<ColaboradorVacacionesConsultaVm>
    {

        public int IdColaborador { get; set; }
        public List<string> Roles { get; set; }
    }
}





