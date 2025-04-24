using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;


namespace NovaExpediente.Application.LenguajesProgramacion.Commands.UpsertLenguajesProgramacion
{
    public class UpsertLenguajesProgramacionCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Versiones { get; set; }

        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertLenguajesProgramacionCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertLenguajesProgramacionCommand request, CancellationToken cancellationToken)
            {
                LenguajeProgramacion entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.LenguajeProgramacions.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new LenguajeProgramacion();

                    _context.LenguajeProgramacions.Add(entity);
                }

                entity.NOMBRE = request.Nombre;
                entity.DESCRIPCION = request.Descripcion;
                entity.VERSIONES = request.Versiones;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDLENGUAJEPROGRAMACION;
            }
        }
    }
}
