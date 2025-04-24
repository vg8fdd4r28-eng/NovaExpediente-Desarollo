using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetCantidadSolicitudes
{
    public class CantidadSolicitudesVm
    {
        public int TotalSolicitudes { get; set; }
        public int SolicitudesEnTransito { get; set; }
    }
}
