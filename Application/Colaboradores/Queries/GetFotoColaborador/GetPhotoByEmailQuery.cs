using MediatR;

namespace NovaExpediente.Application.Colaboradores.Queries.GetFotoColaborador
{
   public class GetPhotoByEmailQuery : IRequest<FotoColaboradorVm>
    {
        public string Email { get; set; }
    }
}





