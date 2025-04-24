using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Cantones.Commands.UpsertCanton
{
    public class UpsertCantonCommand : IRequest<int>
    {
        public int? Id { get; set; }
        public int IdProvincia { get; set; }

        public string Nombre { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertCantonCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertCantonCommand request, CancellationToken cancellationToken)
            {
                Canton entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.Cantones.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Canton();

                    _context.Cantones.Add(entity);
                }

                entity.NOMBRE = request.Nombre;
                entity.IDPROVINCIA = request.IdProvincia;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDCANTON;
            }
        }
    }
}
