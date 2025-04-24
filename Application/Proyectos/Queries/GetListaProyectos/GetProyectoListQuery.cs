using MediatR;

namespace NovaExpediente.Application.Proyectos.Queries.GetListaProyectos
{
   public class GetProyectoListQuery : IRequest<ProyectoListVm>
    {
        public int Id { get; set; }
    }
}





