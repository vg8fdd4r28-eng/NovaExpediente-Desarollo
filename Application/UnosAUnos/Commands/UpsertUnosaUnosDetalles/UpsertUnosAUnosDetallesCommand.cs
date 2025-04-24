using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading.Tasks;
using System.Threading;

namespace NovaExpediente.Application.UnosAUnos.Commands.UpsertUnosaUnosDetalles
{
    public class UpsertUnosAUnosDetallesCommand : IRequest<string>
    {
        public string data { get; set; }
        public class UpsertUnosAUnosDetallesCommandHandler : IRequestHandler<UpsertUnosAUnosDetallesCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertUnosAUnosDetallesCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(UpsertUnosAUnosDetallesCommand request, CancellationToken cancellationToken)
            {
                string result = await _context.UpsertUnosaUnosDetalles(request.data);
                return result;
            }
        }
    }
}
