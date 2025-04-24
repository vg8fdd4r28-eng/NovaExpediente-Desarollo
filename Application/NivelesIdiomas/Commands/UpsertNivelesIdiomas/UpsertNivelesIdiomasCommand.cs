using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
namespace NovaExpediente.Application.NivelesIdiomas.Commands.UpsertNivelesIdiomas
{
    public class UpsertNivelesIdiomasCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public int IdIdioma { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int Orden { get; set; }

        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertNivelesIdiomasCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertNivelesIdiomasCommand request, CancellationToken cancellationToken)
            {
                NivelIdioma entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.NivelIdiomas.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new NivelIdioma();

                    _context.NivelIdiomas.Add(entity);
                }

                entity.IDIDIOMA = request.IdIdioma;
                entity.NOMBRE = request.Nombre;
                entity.DESCRIPCION = request.Descripcion;
                entity.ORDEN = request.Orden;


                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDNIVELIDIOMA;
            }
        }
    }
}
