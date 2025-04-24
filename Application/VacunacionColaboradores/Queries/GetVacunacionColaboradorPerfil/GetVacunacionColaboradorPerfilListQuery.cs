using MediatR;

namespace NovaExpediente.Application.VacunacionColaboradores.Queries.GetVacunacionColaboradorPerfil
{
    public class GetVacunacionColaboradorPerfilListQuery : IRequest<VacunacionColaboradorPerfilVm>
    {
        public long IdColaborador { get; set; }
    }
}