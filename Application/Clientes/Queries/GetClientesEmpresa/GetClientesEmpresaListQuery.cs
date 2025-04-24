using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Clientes.Queries.GetClientesEmpresa
{
    public class GetClientesEmpresaListQuery : IRequest<ClientesEmpresaListVm>
    {
        public int Id { get; set; }
    }
}
