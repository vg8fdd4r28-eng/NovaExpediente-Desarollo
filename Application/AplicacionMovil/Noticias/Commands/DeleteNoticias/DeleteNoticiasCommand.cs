using MediatR;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.AplicacionMovil.Noticias.Commands.DeleteNoticias
{
    public class DeleteNoticiasCommand : IRequest<string>
    {
        public int Id { get; set; }

        public class DeleteNoticiasCommandHandler : IRequestHandler<DeleteNoticiasCommand,string>
        {
            private readonly INovaExpedienteDbContext _context;
            
            public DeleteNoticiasCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(DeleteNoticiasCommand request, CancellationToken cancellationToken)
            {
      

                string res = await _context.InactivarNoticia(request.Id);

                return res;



            }
        }
    }
}
