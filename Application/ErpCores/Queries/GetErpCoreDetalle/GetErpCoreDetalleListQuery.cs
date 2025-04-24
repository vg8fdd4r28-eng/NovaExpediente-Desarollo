using MediatR;

namespace NovaExpediente.Application.ErpCores.Queries.GetErpCoreDetalle
{
   public class GetErpCoreDetalleListQuery : IRequest<ErpCoreDetalleVm>
    {
       public int Id { get; set; }
    }
}





