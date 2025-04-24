using MediatR;

namespace NovaExpediente.Application.ProyectoAmbientes.Queries.GetListaProyectoAmbientes
{
   public class GetProyectoAmbienteListQuery : IRequest<ProyectoAmbienteListVm>
    {
        public int Id { get; set; }
    }
}





