using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Distritos.Commands.UpsertDistrito
{
    public class UpsertDistritoCommand : IRequest<int>
    {
        public int? Id { get; set; }
        public int IdCanton { get; set; }

        public string Nombre { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertDistritoCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertDistritoCommand request, CancellationToken cancellationToken)
            {
                Distrito entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.Distritos.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Distrito();

                    _context.Distritos.Add(entity);
                }

                entity.NOMBRE = request.Nombre;
                entity.IDCANTON = request.IdCanton;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDDISTRITO;
            }
        }
    }
}
