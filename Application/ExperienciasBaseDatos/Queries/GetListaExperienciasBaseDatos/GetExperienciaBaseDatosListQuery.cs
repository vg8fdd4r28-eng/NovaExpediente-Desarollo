using MediatR;

namespace NovaExpediente.Application.ExperienciasBaseDatos.Queries.GetListaExperienciasBaseDatos
{
   public class GetExperienciaBaseDatosListQuery : IRequest<ExperienciaBaseDatosListVm>
    {
        public int Id { get; set; }
    }
}





