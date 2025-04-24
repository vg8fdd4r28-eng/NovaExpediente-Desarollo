using MediatR;

namespace NovaExpediente.Application.FormacionesAcademicas.Queries.GetListaFormacionAcademicaAdjunto
{
   public class GetFormacionAcademicaAdjuntoListQuery : IRequest<FormacionAcademicaAdjuntoListVm>
    {
        public int Id { get; set; }
    }
}





