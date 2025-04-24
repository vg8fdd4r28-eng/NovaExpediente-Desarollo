using MediatR;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace NovaExpediente.Application.CentrosEstudios.Commands.DeleteCentroEstudio
{
    public class DeleteCentroEstudioCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCentroEstudioCommand>
        {
            private readonly INovaExpedienteDbContext _context;

            public DeleteCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteCentroEstudioCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.CentroEstudios
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(CentroEstudio), request.Id);
                }

                _context.CentroEstudios.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
