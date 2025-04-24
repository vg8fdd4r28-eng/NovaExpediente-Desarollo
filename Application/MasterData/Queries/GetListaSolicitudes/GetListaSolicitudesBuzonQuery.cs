using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetListaSolicitudes
{
    public class GetListaSolicitudesBuzonQuery : IRequest<ListaSolicitudesVm>
    {
        public int IdUsuario { get; set; }
        public string Tipo { get; set; } // MiBuzon / Tramitadas
        public string Categoria { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
