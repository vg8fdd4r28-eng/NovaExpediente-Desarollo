using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Provincias.Commands.UpsertProvincia
{
    public class UpsertProvinciaCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Nombre { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertProvinciaCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertProvinciaCommand request, CancellationToken cancellationToken)
            {
                Provincia entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.Provincias.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Provincia();

                    _context.Provincias.Add(entity);
                }

                entity.NOMBRE = request.Nombre;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDPROVINCIA;
            }
        }
    }
}
