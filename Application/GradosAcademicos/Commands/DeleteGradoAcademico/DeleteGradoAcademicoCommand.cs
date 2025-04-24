using MediatR;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.GradosAcademicos.Commands.DeleteGradoAcademico
{
     
   public class DeleteGradoAcademicoCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteGradoAcademicoCommand>
        {
            private readonly INovaExpedienteDbContext _context;

            public DeleteCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteGradoAcademicoCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.GradosAcademicos
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(GradoAcademico), request.Id);
                }

                _context.GradosAcademicos.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
