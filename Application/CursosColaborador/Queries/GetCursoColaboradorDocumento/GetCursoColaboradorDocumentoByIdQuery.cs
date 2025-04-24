using MediatR;

namespace NovaExpediente.Application.CursosColaborador.Queries.GetCursoColaboradorDocumento
{
    public class GetCursoColaboradorDocumentoByIdQuery : IRequest<CursoColaboradorDocumentoVm>
    {
        public int Id { get; set; }
    }
}
