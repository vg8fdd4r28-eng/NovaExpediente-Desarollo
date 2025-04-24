using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.BPMS.Commands.ValidaBPMDependencias
{
    public class ValidaBPMDependenciasCommand : IRequest<string>
    {
        public int Id { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<ValidaBPMDependenciasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(ValidaBPMDependenciasCommand request, CancellationToken cancellationToken)
            {
                string res = "";

                var EnExperiencia = await _context.ExperienciaBPMs
               .Where(c => c.IDBPM == request.Id)
               .ToListAsync(cancellationToken);

                if (EnExperiencia.Count > 0)
                {
                    return res = "No se puede eliminar, la BPM cuenta con experiencias asociadas.";
                }

                return res;




            }
        }
    }
}
