using MediatR;

namespace NovaExpediente.Application.ProyectoTecnologias.Queries.GetListaProyectoTecnologia
{
   public class GetProyectoTecnologiaListQuery : IRequest<ProyectoTecnologiaListVm>
    {
        public int Id { get; set; }
    }
}





