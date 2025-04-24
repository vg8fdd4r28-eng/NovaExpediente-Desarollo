using MediatR;
using Newtonsoft.Json.Linq;

namespace NovaExpediente.Application.AplicacionMovil.Eventos.GetListaEventos
{
    public  class GetListaEventosQuery : IRequest<JObject>
    {
    }
}
