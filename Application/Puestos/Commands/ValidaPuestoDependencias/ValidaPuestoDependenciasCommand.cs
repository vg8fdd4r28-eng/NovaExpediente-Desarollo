using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.Puestos.Commands.ValidaPuestoDependencias
{
    public class ValidaPuestoDependenciasCommand : IRequest<string>
    {
        public int Id { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<ValidaPuestoDependenciasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(ValidaPuestoDependenciasCommand request, CancellationToken cancellationToken)
            {
                string res = "";

               

                var EnColaborador = await _context.Colaboradores
               .Where(c => c.IDPUESTO == request.Id)
               .ToListAsync(cancellationToken);

                if (EnColaborador.Count > 0)
                {
                    return res = "No se puede eliminar, el puesto cuenta con colaboradores asociados.";
                }

          

                return res;




            }
        }
    }
}
