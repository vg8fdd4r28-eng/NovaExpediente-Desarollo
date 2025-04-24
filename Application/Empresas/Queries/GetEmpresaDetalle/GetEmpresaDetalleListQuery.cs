using MediatR;

namespace NovaExpediente.Application.Empresas.Queries.GetEmpresaDetalle
{
    public class GetEmpresaDetalleListQuery : IRequest<EmpresaDetalleVm>
    {
        public int Id { get; set; }
    }
}





