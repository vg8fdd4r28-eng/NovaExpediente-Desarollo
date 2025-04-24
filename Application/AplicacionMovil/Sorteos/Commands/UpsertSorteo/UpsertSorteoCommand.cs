using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.AplicacionMovil.Sorteos.Commands.UpsertSorteo
{
    public class UpsertSorteoCommand : IRequest<string>
    {
        public string Data { get; set; }
        public class UpsertSorteoCommandHandler : IRequestHandler<UpsertSorteoCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;
            public UpsertSorteoCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(UpsertSorteoCommand command, CancellationToken cancellationToken)
            {
                string result = await _context.UpsertSorteo(command.Data);
                return result;
            }
        }
    }
}
