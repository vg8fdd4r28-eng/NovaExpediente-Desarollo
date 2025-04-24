using MediatR;

namespace NovaExpediente.Application.ExperienciasERP.Queries.GetListaExperienciasERP
{
   public class GetExperienciaERPListQuery : IRequest<ExperienciaERPListVm>
    {
        public int Id { get; set; }
    }
}





