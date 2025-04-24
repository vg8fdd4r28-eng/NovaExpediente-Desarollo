using MediatR;

namespace NovaExpediente.Application.ExperienciasBus.Queries.GetListaExperienciasBus
{
   public class GetExperienciaBusListQuery : IRequest<ExperienciaBusListVm>
    {
        public int Id { get; set; }
    }
}





