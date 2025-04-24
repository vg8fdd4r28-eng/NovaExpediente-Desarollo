using System.Collections.Generic;

namespace NovaExpediente.Application.ExperienciasCloud.Queries.GetListaExperienciasCloud
{
    public class ExperienciasCloudVm
    {
        public IList<ExperienciaCloudDto> ExperienciasCloud { get; set; }

        public int Count { get; set; }
    }
}
