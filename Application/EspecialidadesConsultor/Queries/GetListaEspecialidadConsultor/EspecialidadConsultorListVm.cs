using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.EspecialidadesConsultor.Queries.GetListaEspecialidadConsultor
{
    public class EspecialidadConsultorListVm
    {
        public IList<EspecialidadConsultorDto> EspecialidadesConsultor { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
