using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Cursos.Commands.UpsertCursos
{
    public class UpsertCursosCommand : IRequest<int>
    {

        public int? Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertCursosCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertCursosCommand request, CancellationToken cancellationToken)
            {
                Curso entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.Cursos.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Curso();

                    _context.Cursos.Add(entity);
                }

                entity.NOMBRE = request.Nombre;
                entity.DESCRIPCION = request.Descripcion;


                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDCURSO;
            }
        }

    }
}
