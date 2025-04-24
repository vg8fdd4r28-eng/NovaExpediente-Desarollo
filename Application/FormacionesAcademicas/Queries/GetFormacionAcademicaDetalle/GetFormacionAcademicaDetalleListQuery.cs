using MediatR;

namespace NovaExpediente.Application.FormacionesAcademicas.Queries.GetListaFormacionAcademicaDetalle
{
   public class GetFormacionAcademicaDetalleListQuery : IRequest<FormacionAcademicaDetalleVm>
    {
        public int Id { get; set; }
    }
}





