using MediatR;
using System.Collections.Generic;

namespace NovaExpediente.Application.MasterData.Queries.GetResponsablePorPerfil
{
    public class GetResponsablePorPerfilQuery : IRequest<ResponsablePorPerfilVm>
    {
        public int IdColaborador { get; set; }
        public int IdColaboradorSolicitud { get; set; }

        public int idTipoSolicitud { get; set; }

        public string estadoSolicitud { get; set; }


    }
}
