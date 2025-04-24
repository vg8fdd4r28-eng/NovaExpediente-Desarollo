using MediatR;

namespace NovaExpediente.Application.ProyectoLenguajes.Queries.GetListaProyectoLenguaje
{
   public class GetProyectoLenguajeListQuery : IRequest<ProyectoLenguajeListVm>
    {
        public int Id { get; set; }
    }
}





