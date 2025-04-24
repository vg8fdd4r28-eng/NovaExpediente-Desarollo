using MediatR;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.BasesDatos.Commands.DeleteBaseDatos
{
   public class DeleteBaseDatosCommand :IRequest
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteBaseDatosCommand>
        {
            private readonly INovaExpedienteDbContext _context;

            public DeleteCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteBaseDatosCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.BasesDatos
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(BaseDatos), request.Id);
                }

                _context.BasesDatos.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
