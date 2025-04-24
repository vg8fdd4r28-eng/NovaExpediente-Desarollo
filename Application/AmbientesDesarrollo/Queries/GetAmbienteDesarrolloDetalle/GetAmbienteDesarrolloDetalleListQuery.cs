using MediatR;

namespace NovaExpediente.Application.AmbientesDesarrollo.Queries.GetAmbienteDesarrolloDetalle
{
   public class GetAmbienteDesarrolloDetalleListQuery : IRequest<AmbienteDesarrolloDetalleVm>
    {
        public int Id { get; set; }

    }
}





