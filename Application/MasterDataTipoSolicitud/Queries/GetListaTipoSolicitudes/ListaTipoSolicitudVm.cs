using System.Collections.Generic;

namespace NovaExpediente.Application.MasterDataTipoSolicitud.Queries.GetListaTipoSolicitudes
{
    public class ListTipoSolicitudVm
    {
        public IList<TipoSolicitudDto> TiposSolicitudes { get; set; }

        public int count;
    }
}
