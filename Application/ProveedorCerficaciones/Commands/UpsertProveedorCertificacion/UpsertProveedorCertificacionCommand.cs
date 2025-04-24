using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.ProveedorCerficaciones.Commands.UpsertProveedorCertificacion
{
    public class UpsertProveedorCertificacionCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertProveedorCertificacionCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertProveedorCertificacionCommand request, CancellationToken cancellationToken)
            {
                ProveedorCertificacion entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.ProveedorCertificacions.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new ProveedorCertificacion();

                    _context.ProveedorCertificacions.Add(entity);
                }

                entity.Nombre = request.Nombre;
                entity.Descripcion = request.Descripcion;


                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDPROVEEDORCERTIFICACION;
            }
        }
    }
}
