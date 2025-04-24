using MediatR;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.EmpresasCorporacion.Commands.DeleteEmpresasCorporacion
{
    public class DeleteEmpresasCorporacionCommand : IRequest
    {

        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteEmpresasCorporacionCommand>
        {
            private readonly INovaExpedienteDbContext _context;

            public DeleteCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteEmpresasCorporacionCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.EmpresaCorporacions
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(EmpresaCorporacion), request.Id);
                }

                _context.EmpresaCorporacions.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
