using MediatR;

namespace NovaExpediente.Application.ExperienciasCloud.Queries.GetListaExperienciasCloud
{
   public class GetExperienciasCloudListQuery : IRequest<ExperienciasCloudListVm>
    {
        public int Id { get; set; }
    }
}





