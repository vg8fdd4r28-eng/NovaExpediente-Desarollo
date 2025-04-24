using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.AplicacionMovil.Sorteos.Commands.InactivarSorteo
{
    public class InactivarSorteoCommand : IRequest<string>
    {
        public int IdSorteo { get; set; }
        public class InactivarSorteoCommandHandler : IRequestHandler<InactivarSorteoCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;
            public InactivarSorteoCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(InactivarSorteoCommand request, CancellationToken cancellationToken)
            {
                string response = await _context.InactivaSorteo(request.IdSorteo);
                return response;
            }
        }
    }
}
