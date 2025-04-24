using MediatR;
using NovaExpediente.Application.MasterData.Queries.GetColaboradoresAllPorPerfil;
using System.Collections.Generic;

namespace NovaExpediente.Application.MasterData.Queries.GetColaboradoresAllPorPerfil
{
    public class GetColaboradoresAllPorPerfilQuery : IRequest<ColaboradoresAllPorPerfilVm>
    {
        public int IdColaborador { get; set; }
        public List<string> Roles { get; set; }
    }
}
