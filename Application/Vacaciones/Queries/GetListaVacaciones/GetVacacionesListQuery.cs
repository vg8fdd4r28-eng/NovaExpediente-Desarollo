using MediatR;

namespace NovaExpediente.Application.Vacaciones.Queries.GetListaVacaciones
{
    public class GetVacacionesListQuery : IRequest<VacacionesVm>
    {
        public int IdColaborador { get; set; }
        public int IdSolicitudMasterData { get; set; }

    }
}
