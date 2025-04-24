using MediatR;

namespace NovaExpediente.Application.NivelesIdiomas.Queries.GetListaNivelIdiomaXdioma
{
   public class GetNivelIdiomaXdiomaListQuery : IRequest<NivelIdiomaXdiomaListVm>
    {
        public int Id { get; set; }

    }
}





