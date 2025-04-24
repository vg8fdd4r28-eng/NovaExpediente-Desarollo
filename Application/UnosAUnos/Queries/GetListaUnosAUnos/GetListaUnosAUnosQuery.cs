using MediatR;

namespace NovaExpediente.Application.UnosAUnos.Queries.GetListaUnosAUnos
{
    public class GetListaUnosAUnosQuery : IRequest<ListaUnosAUnosVm>
    {
        public int IdDeliveryManager { get; set; }
    }
}
