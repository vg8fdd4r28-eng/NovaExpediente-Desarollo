using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetSolicitudPorAprobar
{
    public class SolicitudPorAprobarVm
    {
        public bool PuedeAprobar { get; set; }
        public string TipoSolicitud { get; set; }
        public int IdTipoSolicitud { get; set; }
        public string MensajeValidacion { get; set; }
    }
}
