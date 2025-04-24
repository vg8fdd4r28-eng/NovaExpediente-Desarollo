using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.AplicacionMovil.Sorteos.Commands.DeleteImagesSorteo
{
    public class DeleteImagesSorteoCommand : IRequest<string>
    {
        public int IdSorteo { get; set; }
        public int IdImagen { get; set; }

        public class DeleteImagesSorteoCommandHandler : IRequestHandler<DeleteImagesSorteoCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;
            public DeleteImagesSorteoCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(DeleteImagesSorteoCommand command, CancellationToken cancellationToken)
            {
                string res = await _context.EliminarSorteoImagen(command.IdSorteo, command.IdImagen);
                return res;
            }
        }
    }
}
