using MediatR;

namespace NovaExpediente.Application.BasesDatos.Queries.GetBaseDatosDetalle
{
   public class GetBaseDatosDetalleListQuery : IRequest<BaseDatosDetalleVm>
    {
       public int Id { get; set; }
    }
}





