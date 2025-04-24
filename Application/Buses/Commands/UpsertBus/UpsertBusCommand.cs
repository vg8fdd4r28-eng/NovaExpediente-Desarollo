using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Buses.Commands.UpsertBus
{
    public class UpsertBusCommand :IRequest<int>
    {
        public int? Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertBusCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertBusCommand request, CancellationToken cancellationToken)
            {
                Bus entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.Buss.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Bus();

                    _context.Buss.Add(entity);
                }

                entity.Nombre = request.Nombre;
                entity.Descripcion = request.Descripcion;


                await _context.SaveChangesAsync(cancellationToken);

                return entity.IdBus;
            }
        }
    }
}
