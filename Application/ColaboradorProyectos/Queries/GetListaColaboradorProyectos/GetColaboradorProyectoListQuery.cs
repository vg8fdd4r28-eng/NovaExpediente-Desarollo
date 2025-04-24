using MediatR;

namespace NovaExpediente.Application.ColaboradorProyectos.Queries.GetListaColaboradorProyectos
{
   public class GetColaboradorProyectoListQuery : IRequest<ColaboradorProyectoListVm>
    {
        public int Id { get; set; }
    }
}





