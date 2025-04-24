using MediatR;

namespace NovaExpediente.Application.HerramientasProyectos.Queries.GetListaHerramientaProyecto
{
   public class GetHerramientaProyectoListQuery : IRequest<HerramientaProyectoListVm>
    {
        public int Id { get; set; }
    }
}





