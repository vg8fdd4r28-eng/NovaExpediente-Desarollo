using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.CentrosEstudios.Commands.ValidaCentroEstudioDependencias
{
    public class ValidaCentroEstudioDependenciasCommand : IRequest<string>
    {
        public int Id { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<ValidaCentroEstudioDependenciasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(ValidaCentroEstudioDependenciasCommand request, CancellationToken cancellationToken)
            {
                string res = "";

                var EnFormacion = await _context.FormacionesAcademicas
               .Where(c => c.IDCENTROESTUDIO == request.Id)
               .ToListAsync(cancellationToken);

                if (EnFormacion.Count > 0)
                {
                    return res = "No se puede eliminar, el centro de estudio cuenta con formaciones académicas asociadas.";
                }

                return res;




            }
        }
    }
}
