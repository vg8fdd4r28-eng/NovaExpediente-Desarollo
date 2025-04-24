using MediatR;

namespace NovaExpediente.Application.Afiliacion.Queries.GetListaAfiliacion
{
   public class GetAfiliacionListQuery : IRequest<AfiliacionListVm>
    {
        public int Id { get; set; }
    }
}





