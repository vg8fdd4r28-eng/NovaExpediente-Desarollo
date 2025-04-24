using MediatR;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.MasterData.Commands.DeleteSolicitudes
{
    public class DeleteSolicitudesCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteSolicitudesCommand>
        {
            private readonly INovaExpedienteDbContext _context;

            public DeleteCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteSolicitudesCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.SolicitudMasterData
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(MasterDataTipoSolicitud), request.Id);
                }

                _context.SolicitudMasterData.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
