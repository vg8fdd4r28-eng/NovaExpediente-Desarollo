using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.AplicacionMovil.Convenios.Commands.UpsertConvenios
{
    public class UpsertConveniosCommand : IRequest<string>
    {
        public string data { get; set; }


        public class UpsertConveniosCommandHandler : IRequestHandler<UpsertConveniosCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;
            private readonly IFireForgetCommandHandler _fireForgetCommand;

            public UpsertConveniosCommandHandler(INovaExpedienteDbContext context, IFireForgetCommandHandler fireForgetCommand)
            {
                _context = context;
                _fireForgetCommand = fireForgetCommand;
            }

            public async Task<string> Handle(UpsertConveniosCommand request, CancellationToken cancellationToken)
            {


                string result = await _context.UpsertConvenios(request.data);

          
                return result;
            }







        }
    }
}
