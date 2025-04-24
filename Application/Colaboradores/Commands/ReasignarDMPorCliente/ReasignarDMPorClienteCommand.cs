using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;



namespace NovaExpediente.Application.Colaboradores.Commands.ReasignarDMPorCliente
{
    public class ReasignarDMPorClienteCommand : IRequest<string>
    {

        public int IdCliente { get; set; }
        public int IdDM { get; set; }
        public int IdDMOld { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<ReasignarDMPorClienteCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(ReasignarDMPorClienteCommand request, CancellationToken cancellationToken)
            {
                string result = "";

                result = await _context.ReasignarDMPorCliente(request.IdCliente,request.IdDM, request.IdDMOld);

                return result;
            }
        }

    }
}
