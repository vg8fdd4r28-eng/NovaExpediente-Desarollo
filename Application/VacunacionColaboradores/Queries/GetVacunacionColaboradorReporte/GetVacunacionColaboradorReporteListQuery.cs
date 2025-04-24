using MediatR;
using System.Collections.Generic;

namespace NovaExpediente.Application.VacunacionColaboradores.Queries.GetVacunacionColaboradorReporte
{
   public class GetVacunacionColaboradorReporteListQuery : IRequest<VacunacionColaboradorReporteVm>
    {

        public int IdColaborador { get; set; }
        public List<string> Roles { get; set; }
    }
}





