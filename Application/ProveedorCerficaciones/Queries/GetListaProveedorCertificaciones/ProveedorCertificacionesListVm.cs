using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ProveedorCerficaciones.Queries.GetListaProveedorCertificaciones
{
    public class ProveedorCertificacionesListVm
    {
        public IList<ProveedorCertificacionesDto> ProveedorCertificaciones { get; set; }

        public int Count { get; set; }
    }
}
