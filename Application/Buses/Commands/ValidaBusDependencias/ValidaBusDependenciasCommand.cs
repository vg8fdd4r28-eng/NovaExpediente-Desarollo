using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.Buses.Commands.ValidaBusDependencias
{
    public class ValidaBusDependenciasCommand : IRequest<string>
    {
        public int Id { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<ValidaBusDependenciasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(ValidaBusDependenciasCommand request, CancellationToken cancellationToken)
            {
                string res = "";

                var EnExperiencia = await _context.ExperienciaBuss
               .Where(c => c.IDBUS == request.Id)
               .ToListAsync(cancellationToken);

                if (EnExperiencia.Count > 0)
                {
                    return res = "No se puede eliminar, el Bus cuenta con experiencias asociadas.";
                }

                return res;




            }
        }
    }
}
