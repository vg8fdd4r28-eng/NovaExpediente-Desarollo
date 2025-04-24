using MediatR;

namespace NovaExpediente.Application.HabilidadesColaborador.Queries.GetListaHabilidadesColaborador
{
   public class GetHabilidadColaboradorListQuery : IRequest<HabilidadColaboradorListVm>
    {
        public int Id { get; set; }
    }
}





