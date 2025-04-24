using MediatR;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresCV
{
    public class GetColaboradoresCVListQuery : IRequest<ColaboradoresCVVm>
    {
        public string Query { get; set; }
        public int TipoCV { get; set; }
    }
}
