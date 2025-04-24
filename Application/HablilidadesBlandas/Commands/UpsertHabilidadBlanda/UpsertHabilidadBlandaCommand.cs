using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.HablilidadesBlandas.Commands.UpsertHabilidadBlanda
{
    public class UpsertHabilidadBlandaCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertHabilidadBlandaCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertHabilidadBlandaCommand request, CancellationToken cancellationToken)
            {
                HabilidadBlanda entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.HabilidadBlandas.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new HabilidadBlanda();

                    _context.HabilidadBlandas.Add(entity);
                }

                entity.NOMBRE = request.Nombre;
                entity.DESCRIPCION = request.Descripcion;


                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDHABILIDADBLANDA;
            }
        }
    }
}
