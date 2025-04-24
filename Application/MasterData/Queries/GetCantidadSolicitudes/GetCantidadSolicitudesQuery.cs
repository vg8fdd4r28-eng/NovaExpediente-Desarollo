using MediatR;

namespace NovaExpediente.Application.MasterData.Queries.GetCantidadSolicitudes
{
    public class GetCantidadSolicitudesQuery : IRequest<CantidadSolicitudesVm>
    {
        public string Categoria { get; set; }
    }
}
