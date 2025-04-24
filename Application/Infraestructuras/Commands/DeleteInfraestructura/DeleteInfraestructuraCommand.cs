using MediatR;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Infraestructuras.Commands.DeleteInfraestructura
{
    public class DeleteInfraestructuraCommand : IRequest
    {

        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteInfraestructuraCommand>
        {
            private readonly INovaExpedienteDbContext _context;

            public DeleteCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteInfraestructuraCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Infraestructuras
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Infraestructura), request.Id);
                }

                _context.Infraestructuras.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
