using MediatR;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace NovaExpediente.Application.ErpCores.Commands.DeleteErpCores
{
    public class DeleteErpCoresCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteErpCoresCommand>
        {
            private readonly INovaExpedienteDbContext _context;

            public DeleteCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteErpCoresCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.ErpCores
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(ErpCore), request.Id);
                }

                _context.ErpCores.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
