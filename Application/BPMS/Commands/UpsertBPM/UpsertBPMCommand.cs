using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;


namespace NovaExpediente.Application.BPMS.Commands.UpsertBPM
{
    public class UpsertBPMCommand : IRequest <int>
    {
        public int? Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertBPMCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertBPMCommand request, CancellationToken cancellationToken)
            {
                BPM entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.BPMs.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new BPM();

                    _context.BPMs.Add(entity);
                }

                entity.Nombre = request.Nombre;
                entity.Descripcion = request.Descripcion;
               

                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDBPM;
            }
        }
    }
}
