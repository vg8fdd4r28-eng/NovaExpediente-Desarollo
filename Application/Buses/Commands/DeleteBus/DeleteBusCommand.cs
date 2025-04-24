using MediatR;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.Buses.Commands.DeleteBus
{
   public class DeleteBusCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteBusCommand>
        {
            private readonly INovaExpedienteDbContext _context;

            public DeleteCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteBusCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Buss
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Bus), request.Id);
                }

                _context.Buss.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
