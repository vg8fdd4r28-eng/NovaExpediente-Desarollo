using MediatR;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Clasificadores.Commands.DeleteClasificador
{
    public class DeleteClasificadorCommand : IRequest
    {

        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteClasificadorCommand>
        {
            private readonly INovaExpedienteDbContext _context;

            public DeleteCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteClasificadorCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Clasificadores
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Clasificador), request.Id);
                }

                _context.Clasificadores.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

    }
}
