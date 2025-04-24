
 using MediatR;

namespace NovaExpediente.Application.Vacunas.Queries.GetVacunaDetalle
{
    public class GetVacunaDetalleListQuery : IRequest<VacunaDetalleVm>
    {
        public long IdVacuna { get; set; }
    }
}