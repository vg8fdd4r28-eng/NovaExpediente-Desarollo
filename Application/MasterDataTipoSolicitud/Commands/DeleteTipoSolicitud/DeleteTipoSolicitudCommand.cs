using MediatR;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.MasterDataTipoSolicitud.Commands.DeleteTipoSolicitud
{
    public class DeleteTipoSolicitudCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteTipoSolicitudCommand>
        {
            private readonly INovaExpedienteDbContext _context;

            public DeleteCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteTipoSolicitudCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.TipoSolicitud
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(TipoSolicitud), request.Id);
                }

                _context.TipoSolicitud.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
