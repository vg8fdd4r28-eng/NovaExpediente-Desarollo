using MediatR;

namespace NovaExpediente.Application.Colaboradores.Queries.GetDetalleColaborador
{
   public class GetColaboradorListKeyQuery : IRequest<ColaboradorVm>
    {
        public int Id { get; set; }
    }
}





