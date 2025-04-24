using MediatR;

namespace NovaExpediente.Application.Referencias.Queries.GetListaReferencias
{
   public class GetReferenciaListQuery : IRequest<ReferenciaListVm>
    {
        public int Id { get; set; }
    }
}





