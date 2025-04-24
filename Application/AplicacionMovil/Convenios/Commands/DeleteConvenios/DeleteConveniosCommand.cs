using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.AplicacionMovil.Convenios.Commands.DeleteConvenios
{
    public class DeleteConveniosCommand : IRequest<string>
    {
        public int Id { get; set; }

        public class DeleteConveniosCommandHandler : IRequestHandler<DeleteConveniosCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;
            
            public DeleteConveniosCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(DeleteConveniosCommand request, CancellationToken cancellationToken)
            {
      

                string res = await _context.InactivarConvenio(request.Id);

                return res;



            }
        }
    }
}
