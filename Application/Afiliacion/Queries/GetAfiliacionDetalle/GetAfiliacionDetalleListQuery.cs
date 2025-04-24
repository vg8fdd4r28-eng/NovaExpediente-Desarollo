using MediatR;
using NovaExpediente.Application.Afiliacion.Queries.AfiliacionDetalle;

namespace NovaExpediente.Application.Afiliacion.Queries.GetListaAfiliacionDetalle
{
   public class GetAfiliacionDetalleListQuery : IRequest<AfiliacionDetalleVm>
    {
        public int Id { get; set; }
    }
}





