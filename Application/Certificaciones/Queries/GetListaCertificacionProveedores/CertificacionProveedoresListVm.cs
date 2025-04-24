using System.Collections.Generic;

namespace NovaExpediente.Application.Certificaciones.Queries.GetListaCertificacionProveedores
{
    public class CertificacionProveedoresListVm
    {
        public IList<CertificacionProveedoresDto> CertificacionesProveedores { get; set; }

        public int Count { get; set; }
    }
}
