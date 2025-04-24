using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetSolicitudPorAprobar
{
    public class GetSolicitudPorAprobarQuery : IRequest<SolicitudPorAprobarVm>
    {
        public int IdSolicitud { get; set; }
        public string Estado { get; set; }
        public string Correo{ get; set; }
    }
}
