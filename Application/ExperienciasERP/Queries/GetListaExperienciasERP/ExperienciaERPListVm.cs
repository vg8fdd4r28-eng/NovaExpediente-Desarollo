using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ExperienciasERP.Queries.GetListaExperienciasERP
{
    public class ExperienciaERPListVm
    {
        public IList<ExperienciaERPDto> ExperienciasERPs { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
