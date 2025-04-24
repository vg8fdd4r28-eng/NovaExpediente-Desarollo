using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Tecnologias.Commands.UpsertTecnologia
{
    public class UpsertTecnologiaCommand : IRequest<int>
    {

            public int? Id { get; set; }

            public string Nombre { get; set; }

            public string Descripcion { get; set; }

            public string Versiones { get; set; }

            public class UpsertCategoryCommandHandler : IRequestHandler<UpsertTecnologiaCommand, int>
            {
                private readonly INovaExpedienteDbContext _context;

                public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
                {
                    _context = context;
                }

                public async Task<int> Handle(UpsertTecnologiaCommand request, CancellationToken cancellationToken)
                {
                    Tecnologia entity;

                    if (request.Id.HasValue && request.Id.Value != 0)
                    {
                        entity = await _context.Tecnologias.FindAsync(request.Id.Value);
                    }
                    else
                    {
                        entity = new Tecnologia();

                        _context.Tecnologias.Add(entity);
                    }

                    entity.NOMBRE = request.Nombre;
                    entity.DESCRIPCION = request.Descripcion;
                    entity.VERSIONES = request.Versiones;

                    await _context.SaveChangesAsync(cancellationToken);

                    return entity.IDTECNOLOGIA;
                }
            }
        }
    }

