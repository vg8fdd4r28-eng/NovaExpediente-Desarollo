using MediatR;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.AplicacionMovil.Noticias.Commands.DeleteImagesNoticias
{
    public class DeleteImagesNoticiasCommand : IRequest<string>
    {
        public int IdNoticia { get; set; }
        public int IdImagen { get; set; }

        public class DeleteImagesNoticiasCommandHandler : IRequestHandler<DeleteImagesNoticiasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public DeleteImagesNoticiasCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(DeleteImagesNoticiasCommand request, CancellationToken cancellationToken)
            {


                string res = await _context.EliminarNoticiaImagen(request.IdNoticia, request.IdImagen);

                return res;



            }
        }
    }
}
