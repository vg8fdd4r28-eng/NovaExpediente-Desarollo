using MediatR;

namespace NovaExpediente.Application.Noticias.Queries.GetNoticiasAPP
{
    public class GetNoticiasAPPQuery : IRequest<string>
    {
        public bool EsPublica { get; set; }
        public int IdIdioma { get; set; }
    }
}