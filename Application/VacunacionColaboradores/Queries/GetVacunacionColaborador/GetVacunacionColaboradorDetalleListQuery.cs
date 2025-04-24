
 using MediatR;

namespace NovaExpediente.Application.VacunacionColaboradores.Queries.GetVacunacionColaboradorDetalle
{
    public class GetVacunacionColaboradorDetalleListQuery : IRequest<VacunacionColaboradorDetalleVm>
    {
        public long IdColaborador { get; set; }
    }
}