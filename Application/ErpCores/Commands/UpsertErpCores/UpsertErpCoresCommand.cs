using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.ErpCores.Commands.UpsertErpCores
{
    public class UpsertErpCoresCommand : IRequest<int>
    {

        public int? Id { get; set; }

        public int IdTipoCore { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public bool EsOpenSource { get; set; }

        public string Tecnologia { get; set; }

        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertErpCoresCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertErpCoresCommand request, CancellationToken cancellationToken)
            {
                ErpCore entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.ErpCores.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new ErpCore();

                    _context.ErpCores.Add(entity);
                }

                entity.IDTIPOCORE = request.IdTipoCore;
                entity.NOMBRE = request.Nombre;
                entity.DESCRIPCION = request.Descripcion;
                entity.ESOPENSOURCE = request.EsOpenSource;
                entity.TECNOLOGIA = request.Tecnologia;


                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDERPCORE;
            }
        }
    }
}
