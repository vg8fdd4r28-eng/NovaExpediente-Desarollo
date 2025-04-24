using MediatR;

namespace NovaExpediente.Application.Colaboradores.Queries.GetFotoColaborador
{
   public class GetPhotoByIdentityCardQuery : IRequest<FotoColaboradorVm>
    {
        public string Identificacion { get; set; }
    }
}





