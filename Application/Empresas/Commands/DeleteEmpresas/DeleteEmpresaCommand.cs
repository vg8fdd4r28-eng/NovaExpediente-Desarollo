using MediatR;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Empresas.Commands.DeleteEmpresas
{
    public class DeleteEmpresaCommand : IRequest
    {

        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteEmpresaCommand>
        {
            private readonly INovaExpedienteDbContext _context;

            public DeleteCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteEmpresaCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Empresas
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Empresa), request.Id);
                }

                _context.Empresas.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
