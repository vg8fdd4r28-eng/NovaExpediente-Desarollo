using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Certificaciones.Queries.GetListaCertificacionConsulta
{
    public class CertificacionConsultaListVm
    {
        public IList<CertificacionConsultaDto> Certificaciones { get; set; }

        public int Count { get; set; }
    }
}
