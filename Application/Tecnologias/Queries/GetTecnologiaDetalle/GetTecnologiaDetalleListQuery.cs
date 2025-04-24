using MediatR;

namespace NovaExpediente.Application.Tecnologias.Queries.GetTecnologiaDetalle
{
   public class GetTecnologiaDetalleListQuery : IRequest<TecnologiaDetalleVm>
    {
       public int Id { get; set; }
    }
}





