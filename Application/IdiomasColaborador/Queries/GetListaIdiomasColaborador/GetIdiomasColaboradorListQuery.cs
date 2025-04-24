using MediatR;

namespace NovaExpediente.Application.IdiomasColaborador.Queries.GetListaIdiomasColaborador
{
   public class GetIdiomasColaboradorListQuery : IRequest<IdiomaColaboradorListVm>
    {
        public int Id { get; set; }
    }
}





