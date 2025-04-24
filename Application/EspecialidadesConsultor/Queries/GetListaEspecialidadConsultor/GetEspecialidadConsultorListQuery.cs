using MediatR;

namespace NovaExpediente.Application.EspecialidadesConsultor.Queries.GetListaEspecialidadConsultor
{
   public class GetEspecialidadConsultorListQuery : IRequest<EspecialidadConsultorListVm>
    {
        public int Id { get; set; }
    }
}





