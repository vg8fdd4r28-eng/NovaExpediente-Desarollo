using MediatR;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresConsulta
{
    public class GetColaboradoresConsultaListQuery : IRequest<ColaboradoresConsultaListVm>
    {
        public string Query { get; set; }
    }
}
