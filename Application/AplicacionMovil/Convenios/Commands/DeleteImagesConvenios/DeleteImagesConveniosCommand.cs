using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.AplicacionMovil.Convenios.Commands.DeleteImagesConvenios
{
    public class DeleteImagesConveniosCommand : IRequest<string>
    {
        public int IdConvenio { get; set; }
        public int IdImagen { get; set; }

        public class DeleteImagesConveniosCommandHandler : IRequestHandler<DeleteImagesConveniosCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public DeleteImagesConveniosCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(DeleteImagesConveniosCommand request, CancellationToken cancellationToken)
            {


                string res = await _context.EliminarConvenioImagen(request.IdConvenio, request.IdImagen);

                return res;



            }
        }
    }
}
