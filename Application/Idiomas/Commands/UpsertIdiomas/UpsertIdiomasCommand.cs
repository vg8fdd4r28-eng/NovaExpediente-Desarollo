using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Idiomas.Commands.UpsertIdiomas
{
    public class UpsertIdiomasCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertIdiomasCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertIdiomasCommand request, CancellationToken cancellationToken)
            {
                Idioma entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.Idiomas.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Idioma();

                    _context.Idiomas.Add(entity);
                }

                entity.NOMBRE = request.Nombre;
                entity.DESCRIPCION = request.Descripcion;
                

                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDIDIOMA;
            }
        }
    }
}
