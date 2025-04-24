using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.CertificacionesColaborador.Queries.GetListaCertificacionColaborador
{
    public class CertificacionColaboradorListVm
    {
        public IList<CertificacionColaboradorDto> CertificacionesColaborador { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
