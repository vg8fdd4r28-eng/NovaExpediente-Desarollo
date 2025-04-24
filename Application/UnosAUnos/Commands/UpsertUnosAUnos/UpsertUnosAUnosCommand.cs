using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading.Tasks;
using System.Threading;

namespace NovaExpediente.Application.UnosAUnos.Commands.UpsertUnosAUnos
{
    public class UpsertUnosAUnosCommand : IRequest<string>
    {
        public string data { get; set; }

        public class UpsertUnosAUnosCommandHandler : IRequestHandler<UpsertUnosAUnosCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertUnosAUnosCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(UpsertUnosAUnosCommand request, CancellationToken cancellationToken)
            {
                string result = await _context.UpsertUnosaUnos(request.data);
                return result;
            }
        }
    
    }
}
