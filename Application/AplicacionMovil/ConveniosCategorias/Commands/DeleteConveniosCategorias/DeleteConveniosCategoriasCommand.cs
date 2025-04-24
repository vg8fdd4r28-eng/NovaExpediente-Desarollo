using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.AplicacionMovil.ConveniosCategorias.Commands.DeleteConveniosCategorias
{
    public class DeleteConveniosCategoriasCommand : IRequest<string>
    {
        public int Id { get; set; }

        public class DeleteConveniosCategoriasCommandHandler : IRequestHandler<DeleteConveniosCategoriasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;
            
            public DeleteConveniosCategoriasCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(DeleteConveniosCategoriasCommand request, CancellationToken cancellationToken)
            {
      

                string res = await _context.InactivarCategoriaConvenios(request.Id);

                return res;



            }
        }
    }
}
