using MediatR;

namespace NovaExpediente.Application.ExperienciasProyectos.Queries.GetExperienciaProyectoDetalle
{
   public class GetExperienciaProyectoDetalleListQuery : IRequest<ExperienciaProyectoDetalleVm>
    {
        public int Id { get; set; }
    }
}





