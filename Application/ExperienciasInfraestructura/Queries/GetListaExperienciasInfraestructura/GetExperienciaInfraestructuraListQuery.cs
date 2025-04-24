using MediatR;

namespace NovaExpediente.Application.ExperienciasInfraestructura.Queries.GetListaExperienciasInfraestructura
{
   public class GetExperienciaInfraestructuraListQuery : IRequest<ExperienciaInfraestructuraListVm>
    {
        public int Id { get; set; }
    }
}





