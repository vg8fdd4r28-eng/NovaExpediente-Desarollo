using MediatR;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.TiposCore.Commands.DeleteTipoCore
{
    public class DeleteTipoCoreCommand : IRequest
    {

        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteTipoCoreCommand>
        {
            private readonly INovaExpedienteDbContext _context;

            public DeleteCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteTipoCoreCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.TipoCores
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(TipoCore), request.Id);
                }

                _context.TipoCores.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

    }
}
