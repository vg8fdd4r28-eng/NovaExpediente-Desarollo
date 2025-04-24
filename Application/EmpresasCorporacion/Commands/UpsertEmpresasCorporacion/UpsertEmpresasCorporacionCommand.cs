using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.EmpresasCorporacion.Commands.UpsertEmpresasCorporacion
{
    public class UpsertEmpresasCorporacionCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertEmpresasCorporacionCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertEmpresasCorporacionCommand request, CancellationToken cancellationToken)
            {
                EmpresaCorporacion entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.EmpresaCorporacions.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new EmpresaCorporacion();

                    _context.EmpresaCorporacions.Add(entity);
                }

                entity.NOMBRE = request.Nombre;
                entity.DESCRIPCION = request.Descripcion;


                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDEMPRESACORPORACION;
            }
        }
    }
}
