using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Paises.Commands.UpsertPais
{
    public class UpsertPaisCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Nombre { get; set; }
        public string CodPais { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertPaisCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertPaisCommand request, CancellationToken cancellationToken)
            {
                Pais entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.Paises.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Pais();

                    _context.Paises.Add(entity);
                }

                entity.NOMBRE = request.Nombre;
                entity.CODPAIS = request.CodPais;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDPAIS;
            }
        }
    }
}
