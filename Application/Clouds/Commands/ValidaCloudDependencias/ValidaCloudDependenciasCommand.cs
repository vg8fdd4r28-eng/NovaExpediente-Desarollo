using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.Clouds.Commands.ValidaCloudDependencias
{
    public class ValidaCloudDependenciasCommand : IRequest<string>
    {
        public int Id { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<ValidaCloudDependenciasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(ValidaCloudDependenciasCommand request, CancellationToken cancellationToken)
            {
                string res = "";

                var ExperienciaCloud = await _context.ExperienciaClouds
               .Where(c => c.IDCLOUD == request.Id)
               .ToListAsync(cancellationToken);

                if (ExperienciaCloud.Count > 0)
                {
                    return res = "No se puede eliminar, el servicio cloud cuenta con experiencias asociadas.";
                }


                return res;




            }
        }
    }
}
