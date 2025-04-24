using MediatR;

namespace NovaExpediente.Application.CursosColaborador.Queries.GetListaCursosColaborador
{
   public class GetCursoColaboradorListQuery : IRequest<CursoColaboradorListVm>
    {
        public int Id { get; set; }
    }
}





