using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;


namespace NovaExpediente.Application.BasesDatos.Commands.UpsertBaseDatos
{
    public class UpsertBaseDatosCommand :IRequest<int>
    {
        public int? Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Versiones { get; set; }

        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertBaseDatosCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertBaseDatosCommand request, CancellationToken cancellationToken)
            {
                BaseDatos entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.BasesDatos.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new BaseDatos();

                    _context.BasesDatos.Add(entity);
                }

                entity.NOMBRE = request.Nombre;
                entity.DESCRIPCION = request.Descripcion;
                entity.VERSIONES = request.Versiones;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDBASEDATOS;
            }
        }
    }
}
