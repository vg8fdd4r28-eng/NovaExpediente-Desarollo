using MediatR;

namespace NovaExpediente.Application.ProyectoBasesDatos.Queries.GetListaProyectoBaseDatos
{
   public class GetProyectoBaseDatosListQuery : IRequest<ProyectoBaseDatosListVm>
    {
        public int Id { get; set; }
    }
}





