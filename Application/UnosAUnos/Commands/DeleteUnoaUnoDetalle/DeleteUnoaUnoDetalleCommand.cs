using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading.Tasks;
using System.Threading;

namespace NovaExpediente.Application.UnosAUnos.Commands.DeleteUnoaUnoDetalle
{
    public class DeleteUnoaUnoDetalleCommand : IRequest<string>
    {
        public int Id { get; set; }

        public class DeleteUnoaUnoDetalleCommandHandler : IRequestHandler<DeleteUnoaUnoDetalleCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public DeleteUnoaUnoDetalleCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(DeleteUnoaUnoDetalleCommand request, CancellationToken cancellationToken)
            {
                string res = await _context.InactivarDetalleUnoaUno(request.Id);
                return res;
            }
        }
    }
}
