using System.Collections.Generic;

namespace NovaExpediente.Application.Clientes.Queries.GetListaClienteReferencias
{
    public class ClienteReferenciasVm
    {
        public IList<ClienteReferenciasDto> ClientesReferencias { get; set; }

        public int Count { get; set; }
    }
}
