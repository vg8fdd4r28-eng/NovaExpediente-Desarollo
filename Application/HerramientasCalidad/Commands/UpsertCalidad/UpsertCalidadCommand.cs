using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.HerramientasCalidad.Commands.UpsertCalidad
{
    public class UpsertCalidadCommand :IRequest<int>
    {
        public int? Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertCalidadCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertCalidadCommand request, CancellationToken cancellationToken)
            {
                Calidad entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.Calidads.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Calidad();

                    _context.Calidads.Add(entity);
                }

                entity.NOMBRE = request.Nombre;
                entity.DESCRIPCION = request.Descripcion;


                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDCALIDAD;
            }
        }
    }
}
