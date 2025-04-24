using MediatR;

namespace NovaExpediente.Application.Referencias.Queries.GetReferenciasDetalle
{
   public class GetReferenciaDetalleListQuery : IRequest<ReferenciaDetalleVm>
    {
        public int Id { get; set; }
    }
}





