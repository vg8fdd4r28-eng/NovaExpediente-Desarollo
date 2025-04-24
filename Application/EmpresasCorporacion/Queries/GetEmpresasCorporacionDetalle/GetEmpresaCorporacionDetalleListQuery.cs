using MediatR;

namespace NovaExpediente.Application.EmpresasCorporacion.Queries.GetEmpresasCorporacionDetalle
{
    public class GetEmpresaCorporacionDetalleListQuery : IRequest<EmpresaCorporacionDetalleVm>
    {
        public int Id { get; set; }
    }
}





