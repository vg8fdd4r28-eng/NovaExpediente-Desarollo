using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.HerramientasCalidad.Commands.ValidaCalidadDependencias
{
    public class ValidaCalidadDependenciasCommand : IRequest<string>
    {
        public int Id { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<ValidaCalidadDependenciasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(ValidaCalidadDependenciasCommand request, CancellationToken cancellationToken)
            {
                string res = "";

               

                var EnExperiencia = await _context.ExperienciaCalidads
               .Where(c => c.IDCALIDAD == request.Id)
               .ToListAsync(cancellationToken);

                if (EnExperiencia.Count > 0)
                {
                    return res = "No se puede eliminar, la tecnología de calidad cuenta con experiencias asociadas.";
                }

                return res;




            }
        }
    }
}
