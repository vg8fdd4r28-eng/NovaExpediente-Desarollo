using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NovaExpediente.Application.AmbientesDesarrollo.Commands.ValidaAmbienteDependencias
{
    public class ValidaAmbienteDependenciasCommand : IRequest<string>
    {
        public int Id { get; set; }


        public class UpsertCategoryCommandHandler : IRequestHandler<ValidaAmbienteDependenciasCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;

            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(ValidaAmbienteDependenciasCommand request, CancellationToken cancellationToken)
            {
                string res = "";

                var EnExperienciaAmbiente = await _context.ExperienciaAmbienteDesarrollos
               .Where(c => c.IDAMBIENTEDESARROLLO == request.Id)
               .ToListAsync(cancellationToken);

                if (EnExperienciaAmbiente.Count > 0)
                {
                    return res = "No se puede eliminar, el ambiente de desarrollo cuenta con experiencias asociadas.";
                }

                var EnProyectoAmbientes = await _context.ProyectoAmbientes
                .Where(c => c.IDAMBIENTEDESARROLLO == request.Id)
                .ToListAsync(cancellationToken);

                if (EnProyectoAmbientes.Count > 0)
                {
                    return res = "No se puede eliminar, el ambiente de desarrollo cuenta con proyectos asociados.";

                }


                return res;




            }
        }
    }
}
