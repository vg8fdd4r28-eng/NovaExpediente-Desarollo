using MediatR;

namespace NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradorDMAsignar
{
   public class GetColaboradorDMAsignarListQuery : IRequest<ColaboradorDMAsignarListVm>
    {
        public int Id { get; set; }
    }
}





