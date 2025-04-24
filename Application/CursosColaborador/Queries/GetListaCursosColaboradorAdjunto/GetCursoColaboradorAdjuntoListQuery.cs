using MediatR;

namespace NovaExpediente.Application.CursosColaborador.Queries.GetListaCursosColaboradorAdjunto
{
   public class GetCursoColaboradorAdjuntoListQuery : IRequest<CursoColaboradorAdjuntoListVm>
    {
        public int Id { get; set; }
    }
}





