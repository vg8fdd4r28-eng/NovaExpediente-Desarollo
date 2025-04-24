using MediatR;

namespace NovaExpediente.Application.Convenios.Queries.GetConveniosAPP
{
    public class GetConveniosAPPQuery : IRequest<string>
    {
        public int IdIdioma { get; set; }
        public int IdCategoria { get; set; }
    }
}