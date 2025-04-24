using MediatR;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
namespace NovaExpediente.Application.ExperienciasProyectos.Commands.DeleteExperienciasProyectos
{
    public class DeleteExperienciaProyectoCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteExperienciaProyectoCommand>
        {
            private readonly INovaExpedienteDbContext _context;

            public DeleteCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteExperienciaProyectoCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.ExperienciaProyectos
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(ExperienciaProyecto), request.Id);
                }

                _context.ExperienciaProyectos.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
