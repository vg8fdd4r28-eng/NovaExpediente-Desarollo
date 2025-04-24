using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Notifications.Models
{
    public enum TipoDenuncia
    {
        Acoso = 1,
        Antisoborno = 2,
        SaludOcupacional = 3
    }

    public class MensajeSolicitudAdministrativa
    {
        public string CorreoResponsable { get; set; }
        public string NombreResponsable { get; set; }
        public TipoNotificacion TipoNotificacion { get; set; }
        public string Solicitante { get; set; }
        public string SolicitanteAprobacion { get; set; }
        public string CorreoSolicitante { get; set; }
        public string Observaciones { get; set; }
        public int IdSolicitud { get; set; }
        public string Estado { get; set; }
        public string DocSolicitud { get; set; }

        public string NombreResponsableActual { get; set; }
        public string ObservacionesAprobador { get; set; }
        public string CorreoAdicionalNotificar { get; set; }
        public string Accion { get; set; }
        public string Denuncia { get; set; }
        public string Denunciado { get; set; }
        public string DescripcionDenunciaTipo { get; set; }
        public int TipoDenuncia { get; set; }
        //public TipoDenuncia TipoDenuncia { get; set; }
    }
}
