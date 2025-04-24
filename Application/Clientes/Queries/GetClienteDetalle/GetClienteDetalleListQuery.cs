using MediatR;

namespace NovaExpediente.Application.Clientes.Queries.GetClienteDetalle
{
   public class GetClienteDetalleListQuery : IRequest<ClienteDetalleVm>
    {
        public int Id { get; set; }

    }
}





