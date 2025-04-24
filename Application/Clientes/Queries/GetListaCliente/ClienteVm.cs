using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Clientes.Queries.GetListaCliente
{
    public class ClienteVm
    {
        public IList<ClienteDto> Clientes { get; set; }

        public int Count { get; set; }
    }
}
