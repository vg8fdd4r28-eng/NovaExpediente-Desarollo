using MediatR;

namespace NovaExpediente.Application.LenguajesProgramacion.Queries.GetLenguajeProgramacionDetalle
{
    public class GetLenguajeProgramacionDetalleListQuery : IRequest<LenguajeProgramacionDetalleVm>
    {
        public int Id { get; set; }
    }
}





