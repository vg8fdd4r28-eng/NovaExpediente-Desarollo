using MediatR;

namespace NovaExpediente.Application.FormacionesAcademicas.Queries.GetListaFormacionAcademica
{
   public class GetFormacionAcademicaListQuery : IRequest<FormacionAcademicaListVm>
    {
        public int Id { get; set; }
    }
}





