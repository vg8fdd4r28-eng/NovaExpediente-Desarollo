using MediatR;

namespace NovaExpediente.Application.Referencias.Queries.GetReferenciasXCliente
{
   public class GetReferenciaXClienteListQuery : IRequest<ReferenciaXClienteListVm>
    {
        public int Id { get; set; }
    }
}





