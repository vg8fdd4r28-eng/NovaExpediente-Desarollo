using MediatR;

namespace NovaExpediente.Application.ProyectoClasificadores.Queries.GetListaProyectoClasificador
{
   public class GetProyectoClasificadorListQuery : IRequest<ProyectoClasificadorListVm>
    {
        public int Id { get; set; }
    }
}





