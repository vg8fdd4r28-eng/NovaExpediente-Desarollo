using System.Collections.Generic;

namespace NovaExpediente.Application.CertificacionesColaborador.Queries.GetListaCertificacionColaboradorAdjunto
{
    public class CertificacionColaboradorAdjuntoListVm
    {
        public IList<CertificacionColaboradorAdjuntoDto> CertificacionesColaborador { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
