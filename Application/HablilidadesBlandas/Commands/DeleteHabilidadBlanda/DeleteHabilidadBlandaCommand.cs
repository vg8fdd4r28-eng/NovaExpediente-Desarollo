using MediatR;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.HablilidadesBlandas.Commands.DeleteHabilidadBlanda
{
    public class DeleteHabilidadBlandaCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteHabilidadBlandaCommand>
        {
            private readonly INovaExpedienteDbContext _context;

            public DeleteCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteHabilidadBlandaCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.HabilidadBlandas
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(HabilidadBlanda), request.Id);
                }

                _context.HabilidadBlandas.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
