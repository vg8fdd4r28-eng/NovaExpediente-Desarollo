using MediatR;

namespace NovaExpediente.Application.FormacionesAcademicas.Queries.GetListaFormacionAcademicaDocumento
{
   public class GetFormacionAcademicaDocumentoByIdQuery : IRequest<FormacionAcademicaDocumentoVm>
    {
        public int Id { get; set; }
    }
}





