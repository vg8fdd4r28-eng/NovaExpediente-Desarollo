using MediatR;

namespace NovaExpediente.Application.ExperienciasAmbientesDesarollo.Queries.GetListaExperienciasAmbientesDesarrollo
{
   public class GetExperienciaAmbienteDesarrolloListQuery : IRequest<ExperienciaAmbienteDesarrolloListVm>
    {
        public int Id { get; set; }
    }
}





