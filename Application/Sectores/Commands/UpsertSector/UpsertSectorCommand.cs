using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;


namespace NovaExpediente.Application.Sectores.Commands.UpsertSector
{
    public class UpsertSectorCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertSectorCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertSectorCommand request, CancellationToken cancellationToken)
            {
                Sector entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.Sectors.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Sector();

                    _context.Sectors.Add(entity);
                }

                entity.Nombre = request.Nombre;
                entity.Descripcion = request.Descripcion;



                await _context.SaveChangesAsync(cancellationToken);

                return entity.IdSector;
            }
        }
    }
}
