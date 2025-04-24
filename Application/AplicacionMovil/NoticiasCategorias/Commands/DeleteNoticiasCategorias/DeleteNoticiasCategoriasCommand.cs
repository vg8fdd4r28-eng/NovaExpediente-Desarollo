using MediatR;
using NovaExpediente.Application.Common.Exceptions;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.AplicacionMovil.NoticiasCategorias.Commands.DeleteNoticiasCategorias
{
    public class DeleteNoticiasCategoriasCommand : IRequest<string>
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteNoticiasCategoriasCommand,string>
        {
            private readonly INovaExpedienteDbContext _context;
            
            public DeleteCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(DeleteNoticiasCategoriasCommand request, CancellationToken cancellationToken)
            {
      

                string res = await _context.InactivarCategoriaNoticias(request.Id);

                return res;



            }
        }
    }
}
