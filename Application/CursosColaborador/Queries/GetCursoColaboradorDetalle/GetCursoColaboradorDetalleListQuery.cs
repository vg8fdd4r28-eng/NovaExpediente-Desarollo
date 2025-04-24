using MediatR;

namespace NovaExpediente.Application.CursosColaborador.Queries.GetCursoColaboradorDetalle
{
   public class GetCursoColaboradorDetalleListQuery : IRequest<CursoColaboradorDetalleVm>
    {
        public int Id { get; set; }
    }
}





