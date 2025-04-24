using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;


namespace NovaExpediente.Application.CentrosEstudios.Commands.UpsertCentroEstudio
{
    public class UpsertCentroEstudioCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }



        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertCentroEstudioCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertCentroEstudioCommand request, CancellationToken cancellationToken)
            {
                CentroEstudio entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.CentroEstudios.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new CentroEstudio();

                    _context.CentroEstudios.Add(entity);
                }

                entity.Nombre = request.Nombre;
                entity.Descripcion = request.Descripcion;


                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDCENTROESTUDIO;
            }
        }
    }
}
