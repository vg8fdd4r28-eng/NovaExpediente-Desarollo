using MediatR;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.AreasOrganizativas.Commands.DeleteAreaOrganizativa
{
    public class DeleteAreaOrganizativaCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteAreaOrganizativaCommand>
        {
            private readonly INovaExpedienteDbContext _context;

            public DeleteCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteAreaOrganizativaCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.AreaOrganizativas
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(AreaOrganizativa), request.Id);
                }

                _context.AreaOrganizativas.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
