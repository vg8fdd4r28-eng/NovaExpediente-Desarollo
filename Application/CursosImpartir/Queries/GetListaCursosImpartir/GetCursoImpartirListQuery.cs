using MediatR;

namespace NovaExpediente.Application.CursosImpartir.Queries.GetListaCursosImpartir
{
   public class GetCursoImpartirListQuery : IRequest<CursoImpartirListVm>
    {
        public int Id { get; set; }
    }
}





