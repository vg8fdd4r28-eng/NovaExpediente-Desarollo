using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.CertificacionesColaborador.Queries.GetListaCertificacionColaborador
{ 
    public class CertificacionColaboradorVm
    {
        public IList<CertificacionColaboradorDto> CertificacionsColaborador { get; set; }

        public int Count { get; set; }
    }
}
