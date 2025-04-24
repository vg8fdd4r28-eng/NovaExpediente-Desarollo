using MediatR;

namespace NovaExpediente.Application.ExperienciasBPM.Queries.GetListaExperienciasBPM
{
   public class GetExperienciaBPMListQuery : IRequest<ExperienciaBPMListVm>
    {
        public int Id { get; set; }
    }
}





