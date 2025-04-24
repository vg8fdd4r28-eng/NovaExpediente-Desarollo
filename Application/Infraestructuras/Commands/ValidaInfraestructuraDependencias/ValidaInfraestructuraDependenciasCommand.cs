using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.Infraestructuras.Commands.ValidaInfraestructuraDependencias
{
    public class ValidaInfraestructuraDependenciasCommand : IRequest<string>
    {
        public int Id { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<ValidaInfraestructuraDependenciasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(ValidaInfraestructuraDependenciasCommand request, CancellationToken cancellationToken)
            {
                string res = "";

               

                var EnExperiencia = await _context.ExperienciaInfraestructuras
               .Where(c => c.IDINFRAESTRUCTURA == request.Id)
               .ToListAsync(cancellationToken);

                if (EnExperiencia.Count > 0)
                {
                    return res = "No se puede eliminar, la infraestructura cuenta con experiencias asociadas.";
                }

          

                return res;




            }
        }
    }
}
