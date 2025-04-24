using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.CertificacionesProveedor.Queries.GetListaProveedorXCertificacion
{ 
    public class CertificacionProveedorXCertificacionVm
    {
        public IList<CertificacionProveedorXCertificacionDto> CertificacionesProveedor { get; set; }

        public int Count { get; set; }
    }
}
