using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Infraestructuras.Commands.UpsertInfraestructura
{
    public class UpsertInfraestructuraCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertInfraestructuraCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertInfraestructuraCommand request, CancellationToken cancellationToken)
            {
                Infraestructura entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.Infraestructuras.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Infraestructura();

                    _context.Infraestructuras.Add(entity);
                }

                entity.NOMBRE = request.Nombre;
                entity.DESCRIPCION = request.Descripcion;


                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDINFRAESTRUCTURA;
            }
        }

    }
}
