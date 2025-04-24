using MediatR;

namespace NovaExpediente.Application.CurriculumVitae.Queries.GetCurriculumVitae
{
    public class GetCurriculumVitaeQuery : IRequest<CurriculumVitaeListVm>
    {
        public int IdColaborador { get; set; }
    }
}
