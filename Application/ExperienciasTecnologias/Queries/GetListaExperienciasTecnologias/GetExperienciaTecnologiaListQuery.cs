using MediatR;

namespace NovaExpediente.Application.ExperienciasTecnologias.Queries.GetListaExperienciasTecnologias
{
   public class GetExperienciaTecnologiaListQuery : IRequest<ExperienciaTecnologiaListVm>
    {
        public int Id { get; set; }
    }
}





