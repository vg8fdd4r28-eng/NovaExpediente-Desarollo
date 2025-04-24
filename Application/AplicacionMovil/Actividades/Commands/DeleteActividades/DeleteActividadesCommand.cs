using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.AplicacionMovil.Actividades.Commands.DeleteActividades
{
    public class DeleteActividadesCommand : IRequest<string>
    {
        public int Id { get; set; }

        public class DeleteActividadesCommandHandler : IRequestHandler<DeleteActividadesCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;
            
            public DeleteActividadesCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(DeleteActividadesCommand request, CancellationToken cancellationToken)
            {
      

                string res = await _context.InactivarActividad(request.Id);

                return res;



            }
        }
    }
}
