using MediatR;

namespace NovaExpediente.Application.Convenios.Queries.GetConvenioDetalleAPP
{
    public class GetConvenioDetalleAPPQuery : IRequest<string>
    {
        public int Id { get; set; }
    }
}