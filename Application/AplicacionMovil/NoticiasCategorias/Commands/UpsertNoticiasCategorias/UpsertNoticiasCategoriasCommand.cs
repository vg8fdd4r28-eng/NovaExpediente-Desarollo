using MediatR;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.AplicacionMovil.NoticiasCategorias.Commands.UpsertNoticiasCategorias
{
    public class UpsertNoticiasCategoriasCommand : IRequest<int>
    {
        public int? Id { get; set; }
        public int IdColaborador { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public bool esEditar { get; set; }

        public class UpsertNoticiasCategoriasCommandHandler : IRequestHandler<UpsertNoticiasCategoriasCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertNoticiasCategoriasCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpsertNoticiasCategoriasCommand request, CancellationToken cancellationToken)
            {
                NoticiaCategoria entity;

                if (request.Id.HasValue && request.Id.Value > 0)
                {
                    entity = await _context.NoticiaCategoria.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new NoticiaCategoria();
                    _context.NoticiaCategoria.Add(entity);
                }

                if (entity == null)
                {
                    throw new NotFoundException(nameof(NoticiaCategoria), request.Id);
                }

                entity.NOMBRE = request.Nombre;
                entity.DESCRIPCION = request.Descripcion;
                

                if (request.esEditar) {
                    entity.IDUSUARIOMODIFICA = request.IdColaborador;
                    entity.FECHAMODIFICACION = DateTime.Now;
                }
                else {
                    entity.ACTIVO = true;
                    entity.IDUSUARIOCREA = request.IdColaborador;
                    entity.FECHACREACION = DateTime.Now;
                }

                await _context.SaveChangesAsync(cancellationToken);
                return entity.IDNOTICIACATEGORIA;
            }
        }
    }
}
