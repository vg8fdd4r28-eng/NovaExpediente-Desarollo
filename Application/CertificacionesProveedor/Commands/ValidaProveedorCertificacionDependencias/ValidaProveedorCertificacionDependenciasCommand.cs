using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.CertificacionesProveedor.Commands.ValidaProveedorCertificacionDependencias
{
    public class ValidaProveedorCertificacionDependenciasCommand : IRequest<string>
    {
        public int Id { get; set; }
        public int IdCatalogo { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<ValidaProveedorCertificacionDependenciasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(ValidaProveedorCertificacionDependenciasCommand request, CancellationToken cancellationToken)
            {
                string res = "";


                var EnCertProveedor = await _context.CertificacionProveedors
             .Where(c => c.IDCERTIFICACION == request.Id && c.IDPROVEEDORCERTIFICACION == request.IdCatalogo)
             .ToListAsync(cancellationToken);


                var EnColaborador = await _context.CertificacionColaboradors
               .Where(c => c.IDCERTIFICACIONPROVEEDOR == EnCertProveedor[0].IDCERTIFICACIONPROVEEDOR)
               .ToListAsync(cancellationToken);

                if (EnColaborador.Count > 0)
                {
                    return res = "No se puede eliminar, la certificación cuenta con colaboradores asociados.";
                }

        

                return res;




            }
        }
    }
}
