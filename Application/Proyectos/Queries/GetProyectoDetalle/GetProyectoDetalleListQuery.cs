using MediatR;

namespace NovaExpediente.Application.Proyectos.Queries.GetProyectoDetalle
{
   public class GetProyectoDetalleListQuery : IRequest<ProyectoDetalleVm>
    {
        public int Id { get; set; }
    }
}





