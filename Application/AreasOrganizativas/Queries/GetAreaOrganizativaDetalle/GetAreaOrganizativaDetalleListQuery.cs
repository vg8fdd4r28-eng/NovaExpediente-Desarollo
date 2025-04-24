using MediatR;


namespace NovaExpediente.Application.AreasOrganizativas.Queries.GetAreaOrganizativaDetalle
{
    public class GetAreaOrganizativaDetalleListQuery : IRequest<AreaOrganizativaDetalleVm>
    {
        public int Id { get; set; }
    }
}
