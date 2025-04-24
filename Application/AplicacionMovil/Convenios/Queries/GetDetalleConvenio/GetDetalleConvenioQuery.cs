using MediatR;

namespace NovaExpediente.Application.AplicacionMovil.Convenios.Queries.GetDetalleConvenio
{
    public class GetDetalleConvenioQuery : IRequest<string>
    {
        public int IdConvenio{ get; set; }
    }
}
