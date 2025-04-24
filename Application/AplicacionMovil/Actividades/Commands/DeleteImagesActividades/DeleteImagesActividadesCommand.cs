using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.AplicacionMovil.Actividades.Commands.DeleteImagesActividades
{
    public class DeleteImagesActividadesCommand : IRequest<string>
    {
        public int IdActividad { get; set; }
        public int IdImagen { get; set; }

        public class DeleteImagesActividadesCommandHandler : IRequestHandler<DeleteImagesActividadesCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public DeleteImagesActividadesCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(DeleteImagesActividadesCommand request, CancellationToken cancellationToken)
            {


                string res = await _context.EliminarActividadImagen(request.IdActividad, request.IdImagen);

                return res;



            }
        }
    }
}
