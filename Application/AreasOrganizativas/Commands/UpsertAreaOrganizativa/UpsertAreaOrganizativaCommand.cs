using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.AreasOrganizativas.Commands.UpsertAreaOrganizativa
{
    public class UpsertAreaOrganizativaCommand: IRequest<int>
    {
        public int? Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertAreaOrganizativaCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertAreaOrganizativaCommand request, CancellationToken cancellationToken)
            {
                AreaOrganizativa entity;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    entity = await _context.AreaOrganizativas.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new AreaOrganizativa();

                    _context.AreaOrganizativas.Add(entity);
                }

                entity.NOMBRE = request.Nombre;
                entity.DESCRIPCION = request.Descripcion;


                await _context.SaveChangesAsync(cancellationToken);

                return entity.IDAREAORGANIZATIVA;
            }
        }
    }
}
