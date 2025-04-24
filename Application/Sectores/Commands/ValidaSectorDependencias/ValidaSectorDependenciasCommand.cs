using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.Sectores.Commands.ValidaSectorDependencias
{
    public class ValidaSectorDependenciasCommand : IRequest<string>
    {
        public int Id { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<ValidaSectorDependenciasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(ValidaSectorDependenciasCommand request, CancellationToken cancellationToken)
            {
                string res = "";

               

                var EnCliente = await _context.Clientess
               .Where(c => c.IDSECTOR == request.Id)
               .ToListAsync(cancellationToken);

                if (EnCliente.Count > 0)
                {
                    return res = "No se puede eliminar, el sector cuenta con clientes asociados.";
                }

                var EnProyecto = await _context.Proyectoss
              .Where(c => c.IDSECTOR == request.Id)
              .ToListAsync(cancellationToken);

                if (EnProyecto.Count > 0)
                {
                    return res = "No se puede eliminar, el sector cuenta con proyectos asociados.";
                }



                return res;




            }
        }
    }
}
