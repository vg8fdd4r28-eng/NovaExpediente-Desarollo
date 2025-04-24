using MediatR;

namespace NovaExpediente.Application.Colaboradores.Queries.GetFotoColaborador
{
   public class GetPhotoByIdQuery : IRequest<FotoColaboradorVm>
    {
        public int Id { get; set; }
    }
}





