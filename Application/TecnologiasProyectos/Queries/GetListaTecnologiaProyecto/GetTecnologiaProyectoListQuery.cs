using MediatR;

namespace NovaExpediente.Application.TecnologiasProyectos.Queries.GetListaTecnologiaProyecto
{
   public class GetTecnologiaProyectoListQuery : IRequest<TecnologiaProyectoListVm>
    {
        public int Id { get; set; }
    }
}





