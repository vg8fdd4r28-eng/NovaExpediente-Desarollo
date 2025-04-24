using MediatR;

namespace NovaExpediente.Application.ExperienciasLenguajes.Queries.GetListaExperienciasLenguajes
{
   public class GetExperienciaLenguajeListQuery : IRequest<ExperienciaLenguajeListVm>
    {
        public int Id { get; set; }
    }
}





