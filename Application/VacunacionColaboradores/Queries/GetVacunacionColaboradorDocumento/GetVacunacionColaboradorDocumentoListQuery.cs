using MediatR;

namespace NovaExpediente.Application.VacunacionColaboradores.Queries.GetVacunacionColaboradorDocumento
{
   public class GetVacunacionColaboradorDocumentoListQuery : IRequest<VacunacionColaboradorDocumentoVm>
    {
        public int IdColaborador { get; set; }
    }
}





