using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.TiposCore.Commands.UpsertTipoCore
{
    public class UpsertTipoCoreCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertTipoCoreCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertTipoCoreCommand request, CancellationToken cancellationToken)
            {
                TipoCore entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.TipoCores.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new TipoCore();

                    _context.TipoCores.Add(entity);
                }

                entity.NOMBRE = request.Nombre;
                entity.DESCRIPCION = request.Descripcion;



                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDTIPOCORE;
            }
        }
    }
}
