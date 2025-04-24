using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ExperienciasLenguajes.Queries.GetListaExperienciasLenguajes
{
    public class ExperienciaLenguajeListVm
    {
        public IList<ExperienciaLenguajeDto> ExperienciaLenguajes { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
