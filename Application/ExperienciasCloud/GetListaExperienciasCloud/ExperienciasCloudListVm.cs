using System.Collections.Generic;

namespace NovaExpediente.Application.ExperienciasCloud.Queries.GetListaExperienciasCloud
{
    public class ExperienciasCloudListVm
    {
        public IList<ExperienciaCloudDto> ExperienciaCloud { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
