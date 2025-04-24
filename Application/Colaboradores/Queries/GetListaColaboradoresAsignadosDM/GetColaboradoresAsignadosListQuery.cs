using MediatR;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradoresAsignadosDM
{
    public class GetColaboradoresAsignadosListQuery : IRequest<ColaboradoresAsignadosDMListVm>
    {
        public int IdDM { get; set; }
    }
}
