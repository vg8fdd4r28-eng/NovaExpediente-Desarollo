using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.AplicacionMovil.Actividades.Commands.UpsertActividades
{
    public class UpsertActividadesCommand : IRequest<string>
    {
        public string data { get; set; }


        public class UpsertActividadesCommandHandler : IRequestHandler<UpsertActividadesCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;
            private readonly IFireForgetCommandHandler _fireForgetCommand;

            public UpsertActividadesCommandHandler(INovaExpedienteDbContext context, IFireForgetCommandHandler fireForgetCommand)
            {
                _context = context;
                _fireForgetCommand = fireForgetCommand;
            }

            public async Task<string> Handle(UpsertActividadesCommand request, CancellationToken cancellationToken)
            {


                string result = await _context.UpsertActividades(request.data);

          
                return result;
            }







        }
    }
}
