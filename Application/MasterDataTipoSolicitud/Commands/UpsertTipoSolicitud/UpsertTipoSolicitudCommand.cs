using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.MasterDataTipoSolicitud.Commands.UpsertTipoSolicitud
{
    public class UpsertTipoSolicitudCommand : IRequest<int>
    {
        public int? Id { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertTipoSolicitudCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertTipoSolicitudCommand request, CancellationToken cancellationToken)
            {
                TipoSolicitud entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.TipoSolicitud.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new TipoSolicitud();

                    _context.TipoSolicitud.Add(entity);
                }

                entity.NOMBRE = request.NOMBRE;
                entity.DESCRIPCION = request.DESCRIPCION;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDTIPOSOLICITUD;
            }
        }
    }
}
