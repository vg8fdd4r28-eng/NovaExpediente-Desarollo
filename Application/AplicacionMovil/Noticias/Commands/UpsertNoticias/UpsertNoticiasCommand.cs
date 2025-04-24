using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.AplicacionMovil.Noticias.Commands.UpsertNoticias
{
    public class UpsertNoticiasCommand : IRequest<string>
    {
        public string data { get; set; }


        public class UpsertNoticiasCommandHandler : IRequestHandler<UpsertNoticiasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;
            private readonly IFireForgetCommandHandler _fireForgetCommand;

            public UpsertNoticiasCommandHandler(INovaExpedienteDbContext context, IFireForgetCommandHandler fireForgetCommand)
            {
                _context = context;
                _fireForgetCommand = fireForgetCommand;
            }

            public async Task<string> Handle(UpsertNoticiasCommand request, CancellationToken cancellationToken)
            {

                // se registra en la base de datos la nueva denuncia
                string result = await _context.UpsertNoticias(request.data);

          
                return result;
            }







        }
    }
}
