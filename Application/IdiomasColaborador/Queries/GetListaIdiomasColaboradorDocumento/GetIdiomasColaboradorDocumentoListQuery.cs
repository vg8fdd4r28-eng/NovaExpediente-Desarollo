using MediatR;

namespace NovaExpediente.Application.IdiomasColaborador.Queries.GetListaIdiomasColaboradorDocumento
{
   public class GetIdiomasColaboradorDocumentoListQuery : IRequest<IdiomaColaboradorDocumentoVm>
    {
        public string Id { get; set; }
    }
}





