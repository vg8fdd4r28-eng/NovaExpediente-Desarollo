using MediatR;

namespace NovaExpediente.Application.IdiomasColaborador.Queries.GetListaIdiomasColaboradorAdjunto
{
   public class GetIdiomasColaboradorAdjuntoListQuery : IRequest<IdiomaColaboradorAdjuntoListVm>
    {
        public int Id { get; set; }
    }
}





