using MediatR;

namespace NovaExpediente.Application.ExperienciasCalidad.Queries.GetListaExperienciasCalidad
{
   public class GetExperienciaCalidadListQuery : IRequest<ExperienciaCalidadListVm>
    {
        public int Id { get; set; }
    }
}





