using System.Collections.Generic;

namespace NovaExpediente.Application.CurriculumVitae.Queries.GetCurriculumVitae
{
    public class CurriculumVitaeListVm
    {
        public List<CurriculumVitaeDto> CurriculumVitaes { get; set; }
        public int count;
    }
}
