using MediatR;

namespace NovaExpediente.Application.NivelesIdiomas.Queries.GetNivelIdiomaDetalle
{
   public class GetNivelIdiomaDetalleListQuery : IRequest<NivelIdiomaDetalleVm>
    {
        public int Id { get; set; }

    }
}





