using System.Collections.Generic;

namespace NovaExpediente.Application.Certificaciones.Queries.GetListaCertificacionProveedores
{
    public class CertificacionProveedoresVm
    {
        public IList<CertificacionProveedoresDto> CertificacionesProveedores { get; set; }

        public int Count { get; set; }
    }
}
