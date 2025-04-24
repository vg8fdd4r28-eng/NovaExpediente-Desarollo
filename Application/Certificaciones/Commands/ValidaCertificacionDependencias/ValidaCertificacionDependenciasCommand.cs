using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.Certificaciones.Commands.ValidaCertificacionDependencias
{
    public class ValidaCertificacionDependenciasCommand : IRequest<string>
    {
        public int Id { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<ValidaCertificacionDependenciasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(ValidaCertificacionDependenciasCommand request, CancellationToken cancellationToken)
            {
                string res = "";

                var EnColaborador = await _context.CertificacionColaboradors
                    .Include(c => c.Certificacion)
                    .Include(c => c.CertificacionProveedor)
               .Where(c => c.IDCERTIFICACION == request.Id)
               .ToListAsync(cancellationToken);

                if (EnColaborador.Count > 0)
                {
                    return res = "No se puede eliminar, la certificación cuenta con colaboradores asociados.";
                }

                var EnProveedor = await _context.CertificacionProveedors
                .Where(c => c.IDCERTIFICACION == request.Id)
                .ToListAsync(cancellationToken);

                if (EnProveedor.Count > 0)
                {
                    return res = "No se puede eliminar, la certificación cuenta con proveedores asociados.";
                }

                return res;




            }
        }
    }
}
