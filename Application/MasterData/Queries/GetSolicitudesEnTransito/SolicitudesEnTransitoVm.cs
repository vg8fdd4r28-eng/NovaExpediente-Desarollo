using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetSolicitudesEnTransito
{
    public class SolicitudesEnTransitoVm
    {
        public IList<SolicitudesEnTransitoDto> Solicitudes { get; set; }

        public int Cantidad { get; set; }
    }
}
