
 using MediatR;

namespace NovaExpediente.Application.VacunasColaboradores.Queries.GetVacunasColaboradorDetalle
{
    public class GetVacunasColaboradorDetalleListQuery : IRequest<VacunasColaboradorDetalleVm>
    {
        public long IdVacunasColaborador { get; set; }
    }
}