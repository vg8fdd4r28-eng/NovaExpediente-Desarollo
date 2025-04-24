using MediatR;

namespace NovaExpediente.Application.Nomina.Queries.GetSaldoVacaciones
{
    public class GetVacacionesNominaListQuery : IRequest<VacacionesNominaVm>
    {
        public int IdNomina { get; set; }
    }
}
