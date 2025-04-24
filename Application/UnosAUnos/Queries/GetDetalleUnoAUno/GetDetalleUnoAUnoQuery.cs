using MediatR;

namespace NovaExpediente.Application.UnosAUnos.Queries.GetDetalleUnoAUno
{
    public class GetDetalleUnoAUnoQuery : IRequest<string>
    {
        public int IdUnoAUno { get; set; }
    }
}
