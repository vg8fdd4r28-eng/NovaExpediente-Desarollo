using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Colaboradores.Queries.GetColaboradorEsNovaShore
{
    public class GetColaboradorEsNovaShoreQuery : IRequest<bool>
    {
        public string Email { get; set; }
    }
}
