using System.Collections.Generic;

namespace NovaExpediente.Application.Clientes.Queries.GetListaClienteReferencias
{
    public class ClienteReferenciasListVm
    {
        public IList<ClienteReferenciasDto> ClientesReferencias { get; set; }

        public int Count { get; set; }
    }
}
