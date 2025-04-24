using MediatR;

namespace NovaExpediente.Application.Colaboradores.Queries.GetColaboradorVacaciones
{
   public class GetColaboradorVacacionesListKeyQuery : IRequest<ColaboradorVacacionesVm>
    {
        public int Id { get; set; }
    }
}





