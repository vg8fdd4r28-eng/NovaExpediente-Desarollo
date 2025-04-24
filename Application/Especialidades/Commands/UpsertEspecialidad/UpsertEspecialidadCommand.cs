using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;


namespace NovaExpediente.Application.Especialidades.Commands.UpsertEspecialidad
{
    public class UpsertEspecialidadCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertEspecialidadCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertEspecialidadCommand request, CancellationToken cancellationToken)
            {
                Especialidad entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.Especialidades.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Especialidad();

                    _context.Especialidades.Add(entity);
                }

                entity.NOMBRE = request.Nombre;
                entity.DESCRIPCION = request.Descripcion;
               

                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDESPECIALIDAD;
            }
        }
    }
}
