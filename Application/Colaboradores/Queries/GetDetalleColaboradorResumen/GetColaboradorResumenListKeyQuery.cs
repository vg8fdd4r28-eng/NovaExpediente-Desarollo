using MediatR;

namespace NovaExpediente.Application.Colaboradores.Queries.GetDetalleColaboradorResumen
{
   public class GetColaboradorResumenListKeyQuery : IRequest<ColaboradorResumenVm>
    {
        public int Id { get; set; }
    }
}





