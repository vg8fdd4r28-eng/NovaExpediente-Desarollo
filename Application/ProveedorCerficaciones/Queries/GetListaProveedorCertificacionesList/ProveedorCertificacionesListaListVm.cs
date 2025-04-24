using System.Collections.Generic;

namespace NovaExpediente.Application.ProveedorCerficaciones.Queries.GetListaProveedorCertificacionesList
{
    public class ProveedorCertificacionesListaListVm
    {
        public IList<ProveedorCertificacionesListaDto> ProveedorCertificaciones { get; set; }

        public int Count { get; set; }
    }
}
