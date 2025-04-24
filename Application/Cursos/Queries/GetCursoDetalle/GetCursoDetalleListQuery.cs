using MediatR;

namespace NovaExpediente.Application.Cursos.Queries.GetCursoDetalle
{
    public class GetCursoDetalleListQuery : IRequest<CursoDetalleVm>
    {
        public int Id { get; set; }
    }
}
