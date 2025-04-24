using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Colaboradores.Commands.ActualizarDatosAPP
{
    public class ActualizarDatosAPPCommand : IRequest<string>
    {
        public string data { get; set; }

        public class ActualizarDatosCommandHandler : IRequestHandler<ActualizarDatosAPPCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public ActualizarDatosCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(ActualizarDatosAPPCommand request, CancellationToken cancellationToken)
            {
                string result = await _context.ActualizaDatosPersonalesAPP(request.data);
                return result;
            }
        }
    }
}