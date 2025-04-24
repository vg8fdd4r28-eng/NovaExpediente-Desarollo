using MediatR;

namespace NovaExpediente.Application.ExperienciasProyectos.Queries.GetListaExperienciaProyecto
{
   public class GetExperienciaProyectoListQuery : IRequest<ExperienciaProyectoListVm>
    {
        public int Id { get; set; }
    }
}





