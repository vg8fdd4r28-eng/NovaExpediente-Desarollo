using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Clientes.Queries.GetListaCliente
{
    public class ClienteListVm
    {
        public IList<ClienteDto> Clientes { get; set; }

        public int Count { get; set; }
    }
}
