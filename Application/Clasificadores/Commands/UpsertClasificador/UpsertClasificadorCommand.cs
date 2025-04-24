using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Domain.Entities;
using NovaExpediente.Application.Common.Interfaces;



namespace NovaExpediente.Application.Clasificadores.Commands.UpsertClasificador
{
    public class UpsertClasificadorCommand : IRequest<int>
    {

        public int? Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int Orden { get; set; }

        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertClasificadorCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertClasificadorCommand request, CancellationToken cancellationToken)
            {
               Clasificador entity;

                if (request.Id.HasValue)
                {
                    entity = await _context.Clasificadores.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Clasificador();

                    _context.Clasificadores.Add(entity);
                }

                entity.Nombre = request.Nombre;
                entity.Descripcion = request.Descripcion;
          

                await _context.SaveChangesAsync(cancellationToken);

                return entity.IdClasificador;
            }
        }

    }
}
