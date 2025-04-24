using MediatR;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ProveedorCerficaciones.Commands.DeleteProveedorCertificacion
{
    public class DeleteProveedorCertificacionCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteProveedorCertificacionCommand>
        {
            private readonly INovaExpedienteDbContext _context;

            public DeleteCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteProveedorCertificacionCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.ProveedorCertificacions
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(ProveedorCertificacion), request.Id);
                }

                _context.ProveedorCertificacions.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
