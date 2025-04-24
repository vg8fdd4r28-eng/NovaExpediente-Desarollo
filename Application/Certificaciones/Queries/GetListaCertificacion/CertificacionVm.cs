using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Certificaciones.Queries.GetListaCertificacion
{
    public class CertificacionVm
    {
        public IList<CertificacionDto> Certificaciones { get; set; }

        public int Count { get; set; }
    }
}
