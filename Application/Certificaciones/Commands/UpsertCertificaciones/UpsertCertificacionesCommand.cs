using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Certificaciones.Commands.UpsertCertificaciones
{
    public class UpsertCertificacionesCommand : IRequest<int>
    {

        public int? Id { get; set; }

        public string CodigoCertificacion { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }




        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertCertificacionesCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertCertificacionesCommand request, CancellationToken cancellationToken)
            {
                Certificacion entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.Certificacions.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Certificacion();

                    _context.Certificacions.Add(entity);
                }

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Certificacion), request.Id);
                }

                entity.CODIGOCERTIFICACION = request.CodigoCertificacion;
                entity.NOMBRE = request.Nombre;
                entity.DESCRIPCION = request.Descripcion;


                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDCERTIFICACION;
            }
        }

    }
}
