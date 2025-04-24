using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.GradosAcademicos.Commands.UpsertGradoAcademico
{
    public class UpsertGradoAcademicoCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int Orden { get; set; }

        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertGradoAcademicoCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertGradoAcademicoCommand request, CancellationToken cancellationToken)
            {
                GradoAcademico entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.GradosAcademicos.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new GradoAcademico();

                    _context.GradosAcademicos.Add(entity);
                }

                entity.Nombre = request.Nombre;
                entity.Descripcion = request.Descripcion;
                entity.Orden = request.Orden;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.IdGradoAcademico;
            }
        }
    }
}
