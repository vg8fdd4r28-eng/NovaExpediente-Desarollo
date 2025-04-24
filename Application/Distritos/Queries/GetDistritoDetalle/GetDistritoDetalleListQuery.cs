using MediatR;

namespace NovaExpediente.Application.Distritos.Queries.GetDistritoDetalle
{
    public class GetDistritoDetalleListQuery : IRequest<DistritoDetalleVm>
    {
       public int Id { get; set; }
    }
}





