using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Puestos.Commands.UpsertPuesto
{
    public class UpsertPuestoCommand :IRequest <int>
    {
        public int? Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public bool EsDm { get; set; }

        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertPuestoCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertPuestoCommand request, CancellationToken cancellationToken)
            {
                Puesto entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.Puestos.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Puesto();

                    _context.Puestos.Add(entity);
                }

                entity.NOMBRE = request.Nombre;
                entity.DESCRIPCION = request.Descripcion;
                entity.ES_DM = request.EsDm;


                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDPUESTO;
            }
        }
    }
}
