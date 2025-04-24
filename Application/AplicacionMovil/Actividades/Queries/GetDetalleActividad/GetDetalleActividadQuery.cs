using MediatR;

namespace NovaExpediente.Application.AplicacionMovil.Actividades.Queries.GetDetalleActividad
{
    public class GetDetalleActividadQuery : IRequest<string>
    {
        public int IdActividad { get; set; }
    }
}
