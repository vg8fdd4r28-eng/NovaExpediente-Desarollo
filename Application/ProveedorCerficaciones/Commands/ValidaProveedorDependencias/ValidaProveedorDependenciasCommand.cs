using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.ProveedorCerficaciones.Commands.ValidaProveedorDependencias
{
    public class ValidaProveedorDependenciasCommand : IRequest<string>
    {
        public int Id { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<ValidaProveedorDependenciasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(ValidaProveedorDependenciasCommand request, CancellationToken cancellationToken)
            {
                string res = "";

               

                var EnExperiencia = await _context.CertificacionProveedors
               .Where(c => c.IDPROVEEDORCERTIFICACION == request.Id)
               .ToListAsync(cancellationToken);

                if (EnExperiencia.Count > 0)
                {
                    return res = "No se puede eliminar, el proveedor cuenta con certificaciones asociadas.";
                }

          

                return res;




            }
        }
    }
}
